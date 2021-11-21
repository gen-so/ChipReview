namespace BlazorApp.Shared
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
            public const string GetReviewListApi = "https://chipreviewapi.azurewebsites.net/api/GetReview";
            public const string AddNewReviewApi = "https://chipreviewapi.azurewebsites.net/api/AddNewReview"; //todo impliment
            
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
            public const string AccountList = "chip-review-site-data/accountList.xml";
            public const string DomainLog = "chip-review-site-data/domainLog.xml";
            public const string ReviewList = "chip-review-site-data/review-list.xml";
            public const string Config = "chip-review-site-data/config.xml";
            public const string AppLog = "chip-review-site-data/appLog.xml";
        }
    }

}
