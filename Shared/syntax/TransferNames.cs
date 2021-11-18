namespace BlazorApp.Shared
{

    /// <summary>
    /// Hardcoded names that are used between client & API communications
    /// </summary>
    public static class TransferNames
    {
        public static class ClientToApi
        {
            public static string IP = "IP";
            public static string TopDomain = "TopDomain";
            public static string SubDomain = "SubDomain";
            public static string Key1 = "Key1";
            public static string Username = "Username";
            public static string Email = "Email";
            public static string Chip = "Chip"; 
            public static string Vendor = "Vendor";
        }
        public static class ApiToServer
        {
            public static string IP = "IP";
            public static string TopDomain = "TopDomain";
            public static string SubDomain = "SubDomain";
            public static string Key1 = "Key1";
        }
        public static class ApiToClient
        {
            public static string Root = "Root";
            public static string Status = "Status";
            public static string Message = "Message";
            public static string ExtraInfo = "ExtraInfo";
            public static string ReviewList = "ReviewList";
            public static string ReviewText = "Review";
            public static string Username = "Username";
            public static string Time = "Time";
            public static string Rating = "Rating";
            public static string Chip = "Chip";
            public static string Vendor = "Vendor";
        }

    }
}