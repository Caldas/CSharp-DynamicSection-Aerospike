using Aerospike.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace VTEX.Configuration.DynamicSection.Aerospike
{
    public class AerospikeConfigurationReloadPlugin : IConfigurationReloadPlugin
    {
        private static readonly object lockState = new object();
        private static readonly UTF8Encoding utf8Encoding = new UTF8Encoding();

        private static AsyncClient client = null;

        private static void CheckClient(string hosts, int port)
        {
            if (client == null)
            {
                //Se o client for nulo, há um lock para que apenas 1 thread possa chamar a próxima parte de criação do cliente
                lock (lockState)
                {
                    //Aqui a uma nova checagem pois uma thread que estava parada do lock pode evitar essa passo, caso a thread anterior já tenha criado o client 
                    if (client == null)
                    {
                        try
                        {
                            List<Host> aerospikeHosts = new List<Host>();
                            foreach (string hostIp in hosts.Split(new char[]{','}))
                            {
                                aerospikeHosts.Add(new Host(hostIp, port));
                            }
                            client = new AsyncClient(
                                new AsyncClientPolicy()
                                {
                                    asyncMaxCommandAction = MaxCommandAction.REJECT,
                                    failIfNotConnected = false
                                },
                                aerospikeHosts.ToArray());
                        }
                        catch
                        {
                            //Fail safe if any problem
                        }
                    }
                }
            }

        }

        public event EventHandler<Tuple<string, object>> PropertyLoaded = delegate { };

        public void GetLastestPropertiesValues(ConfigurationSection configurationSection)
        {
            try
            {
                var clusterIps = configurationSection.ElementInformation.Properties[AerospikeConfigurationSection.Property_ClusterIps].Value.ToString();
                var port = (int)configurationSection.ElementInformation.Properties[AerospikeConfigurationSection.Property_Port].Value;
                CheckClient(clusterIps, port);

                foreach (PropertyInformation item in configurationSection.ElementInformation.Properties)
                {
                    string s = item.Name;
                    int i = s.Length;

                    //TODO: Implement flux below:
                    //* Define key, using namespace and set properties
                    //* Get item.Name value
                    //** If null set current value
                    //** Else return stored value
                    //* If returned values different from current, call'PropertyLoaded' event
                }
            }
            catch
            {
                //Fail safe
            }
        }

        //private Key GetKey(string hash)
        //{
        //    string setName = string.Format("{0}_{1}", VTEXDiagnosticsManager.Instance.AppName, VTEXDiagnosticsManager.Instance.AppVersion.Replace('.', '_').Replace('-', '_'));
        //    return new Key("evidence", setName, hash);
        //}
    }
}