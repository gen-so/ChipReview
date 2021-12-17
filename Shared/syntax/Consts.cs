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
            public const string GetReviewList = "https://chipreviewapi.azurewebsites.net/api/GetReview";
            public const string GetReviewListAll = "https://chipreviewapi.azurewebsites.net/api/GetReviewAll";
            public const string AddNewReview = "https://chipreviewapi.azurewebsites.net/api/AddNewReview";
            public const string DeleteReview = "https://chipreviewapi.azurewebsites.net/api/DeleteReview";
            public const string DeleteAccount = "https://api-ddns-genso.azurewebsites.net/api/DeleteAccount";

            public const string Config = "data\\config.xml";
            public const string IpCache = "data\\ip-cache.xml";
        }




        /// <summary>
        /// Azure Function accesses the files stored in container via paths below
        /// Since azure function is linked to storage account, full URL to storage not needed
        /// </summary>
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
