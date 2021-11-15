using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Api
{
    public static class Consts
    {
        public static class Server
        {

            public const string Config = "data\\config.xml";
        }


        public static class Client
        {
            public const string UpdateDomainApi = "https://api-ddns-genso.azurewebsites.net/api/UpdateDomain";
            public const string ListDomainsApi = "https://api-ddns-genso.azurewebsites.net/api/ListDomain";
            public const string AddDomainApi = "https://api-ddns-genso.azurewebsites.net/api/AddDomain";
            public const string DeleteDomainApi = "https://api-ddns-genso.azurewebsites.net/api/DeleteDomain";
            public const string CheckAccountApi = "https://api-ddns-genso.azurewebsites.net/api/CheckAccount";
            public const string CreateAccountApi = "https://api-ddns-genso.azurewebsites.net/api/CreateAccount";
            public const string DeleteAccountApi = "https://api-ddns-genso.azurewebsites.net/api/DeleteAccount";

            public const string Config = "data\\config.xml";
            public const string IpCache = "data\\ip-cache.xml";
        }


        public static class API
        {
            //PATHS TO DATA FILES STORED IN AZURE DATA BLOB 
            public const string DomainList = "data/domainList.xml";
            public const string AccountList = "data/accountList.xml";
            public const string DomainLog = "data/domainLog.xml";
            public const string ReviewList = "data/review-list.xml";
            public const string Config = "data/config.xml";
            public const string AppLog = "data/appLog.xml";
        }
    }

}
