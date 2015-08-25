using System;

namespace DynamicSection.Aerospike
{
    public class ConfigurationSectionException : Exception
    {
        public ConfigurationSectionException()
            : base("Cluster configuration on app/web.config doesn't have Ip Or Dns set.")
        {
        }
    }
}