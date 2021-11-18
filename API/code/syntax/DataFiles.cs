
using System.Xml.Linq;


namespace API
{

    /// <summary>
    /// Library to store crosstalk syntax & XML file data names
    /// </summary>
    public static class DataFiles
    {
        public static class Client
        {
            public static class IpCache
            {
                public static string OldIP = "OldIP";
            }

            public static class Config
            {
                public static string Interval = "Interval";
                public static string API = "API";
                public static string DefaultIp = "DefaultIp";
                public static string DNServerAddress = "DNServerAddress";
                public static string Key1 = "Key1";
                public static string Domain = "Domain";
                public static string CheckedDomain = "CheckedDomain";
                public static string SubDomain = "SubDomain";
                public static string TopDomain = "TopDomain";
            }
        }
        public static class Server
        {

            public static class Config
            {
                public const string ListenPort = "ListenPort";
                public const string ServerName = "ServerName";
            }
        }
        public static class API
        {

            //accountList.xml
            public static class AccountList
            {
                public const string ID = "ID";
                public const string KEY2 = "KEY2";
                public const string LOCK = "LOCK";
                public const string DomainList = "DomainList";
                public const string Domain = "Domain";
                public const string Record = "Record";
                public const string Username = "Username";
                public const string Email = "Email";
            }

            //review-list.xml
            public static class ReviewList
            {
                public const string Review = "Review";
                public const string Chip = "Chip";
                public const string Vendor = "Vendor";
                public const string Rating = "Rating";
                public const string Time = "Time";
                public const string Username = "Username";

            }

            //appLog.xml
            public static class AppLog
            {
                public const string Debug = "Debug";
                public const string Source = "Source";
                public const string FileName = "FileName";
                public const string SourceColNumber = "SourceColNumber";
                public const string MethodName = "MethodName";
                public const string Error = "Error";
                public const string Message = "Message";
                public const string SourceLineNumber = "SourceLineNumber";
                public const string Time = "Time";
            }

            //domainList.xml
            public static class DomainList
            {
                public const string Domain = "Domain";
                public const string IP = "IP";
                public const string Time = "Time";
                public const string SubDomain = "SubDomain";
                public const string Record = "Record";
            }

            //domainLog.xml
            public static class DomainLog
            {
                public const string Domain = "Domain";
                public const string IP = "IP";
                public const string Time = "Time";
                public const string SubDomain = "SubDomain";

            }

            //config.xml
            public static class Config
            {
                public const string DNServerDomain = "DNServerDomain";
                public const string DNServerPort = "DNServerPort";
                public const string DefaultIp = "DefaultIp";
                public const string TopDomain = "TopDomain";
            }
        }

    }
}
