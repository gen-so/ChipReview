using BlazorApp.Shared;
using System.Linq;



namespace API
{
    public class ApiConfigManager : ConfigManager
    {
        public ApiConfigManager(Data config) : base(config)
        {
        }

        /// <summary>
        /// Gets the DNS server's domain address (for connecting)
        /// </summary>
        public string getDnsAddress() => _config.getValue<string>(DataFiles.API.Config.DNServerDomain);


        /// <summary>
        /// Gets the port number to connect to the DNS server
        /// </summary>
        public int getDnsPort() => _config.getValue<int>(DataFiles.API.Config.DNServerPort);
        /// <summary>
        /// Check if the inputed domain is in the top domain list (config file)
        /// </summary>
        public bool isDomainInTopList(string domain)
        {
            //get the top domain list
            var topDomainRecord = _config.getRecord(DataFiles.API.Config.TopDomain);
            var domainList = topDomainRecord.Elements();

            //search the list for a match
            var found = from re in domainList where re.Value == domain select re;

            //if found in list, let caller know found in list
            return found.Any() ? true : false;

        }

    }
}
