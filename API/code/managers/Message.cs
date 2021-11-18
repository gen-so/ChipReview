using BlazorApp.Shared;
using System.Xml.Linq;

namespace API
{
    /// <summary>
    /// Centralized place to store/generate user messages
    /// These are messages from the program that will be shown to the user
    /// </summary>
    public static class Message //TODO might need to change name to MessageManager
    {

        /** PUBLIC METHODS **/

        /// <summary>
        /// Converts standardized status update message to a XML string
        /// Data inside info is normal string message
        /// </summary>
        public static string create(string status, string info, string extraInfo = "")
        {

            XElement dataPackage = new XElement(TransferNames.ApiToClient.Root,
                new XElement(TransferNames.ApiToClient.Status, status),
                new XElement(TransferNames.ApiToClient.Message, info),
                new XElement(TransferNames.ApiToClient.ExtraInfo, extraInfo)
            );

            //convert xml to string
            string dataString = TransferData.xmlToString(dataPackage);

            //return string to caller
            return dataString;
        }

        /// <summary>
        /// Converts standardized status update message to a XML string
        /// Data inside info is an xml element
        /// </summary>
        public static string create(string status, XElement info)
        {

            XElement dataPackage = new XElement(TransferNames.ApiToClient.Root,
                new XElement(TransferNames.ApiToClient.Status, status),
                new XElement(TransferNames.ApiToClient.Message, info)
            );

            //convert xml to string
            string dataString = TransferData.xmlToString(dataPackage);

            //return string to caller
            return dataString;
        }


    }
}
