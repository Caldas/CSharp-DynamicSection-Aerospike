using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTEX.Configuration.DynamicSection.Aerospike
{
    public class AerospikeConfigurationSection : DynamicConfigurationSection
    {
        internal const string Property_ClusterIps = "clusterIps";
        internal const string Property_Port = "port";

        /// <summary>
        /// Get or set a string array splitted by ',' with aerospike nodes ips
        /// </summary>
        [ConfigurationProperty(Property_ClusterIps, IsRequired = true)]
        public string ClusterIps
        {
            get
            {
                return this[Property_ClusterIps].ToString();
            }
            set
            {
                this[Property_ClusterIps] = value;
            }
        }

        /// <summary>
        /// Get or set aerospike nodes port
        /// </summary>
        [ConfigurationProperty(Property_Port, IsRequired = false, DefaultValue = 3000)]
        public int Port
        {
            get
            {
                return int.Parse(this[Property_Port].ToString());
            }
            set
            {
                this[Property_Port] = value;
            }
        }

        /// <summary>
        /// Get or set aerospike namespace where data will be stored/retreived
        /// </summary>
        [ConfigurationProperty("namespace", IsRequired = true)]
        public string Namespace
        {
            get
            {
                return this["namespace"].ToString();
            }
            set
            {
                this["namespace"] = value;
            }
        }
    }
}