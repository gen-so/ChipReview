using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Client
{

    /// <summary>
    /// A simple encapsulation of the data received after a request from server
    /// Example after "add domain" request is sent to API from Client,
    /// the response data can be stored in this form
    /// </summary>
    public class Response
    {
        public Response(string status, string message, string extraInfo)
        {
            Status = status;
            Message = message;
            ExtraInfo = extraInfo;
        }

        public string Status { get; }
        public string Message { get; }
        public string ExtraInfo { get; }


        /// <summary>
        /// Returns true if the response failed
        /// </summary>
        public bool IsFail() => Status == "Fail";
        /// <summary>
        /// Returns true if the response passed
        /// </summary>
        public bool IsPass() => Status == "Pass";



        //OVERRIDES


        public override string ToString() => $"Status :{Status}\nMessage :{Message}\nExtraInfo :{ExtraInfo}";
    }
}
