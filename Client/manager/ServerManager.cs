using BlazorApp.Shared;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BlazorApp.Client
{ 
    /// <summary>
    /// Encapsulates all thing todo with server (API)
    /// </summary>
public class ServerManager
    {

        /** EVENTS **/


        /** PUBLIC METHODS **/



        /// <summary>
        /// Delete the user account at the server
        /// If unable to delete account, returns false, if success true
        /// </summary>
        public async Task<Response> DeleteAccount(string key1)
        {
            //package data to be sent to API server
            var request = new TransferData();
            request.addData(TransferNames.ClientToApi.Key1, key1);

            //process request & handle all related events
            var result = await processRequest(Consts.Client.DeleteAccountApi, request);

            //return the result of the request processing
            return result;
        }



        /// <summary>
        /// Gets lists all domains that are linked to the user account
        /// This is where review gets converted from raw XML to parsed Review object
        /// </summary>
        public static async Task<List<Review>> GetReviewList(string chip, string vendor)
        {
            //package data to be sent to API server
            var request = new TransferData();
            request.addData(TransferNames.ClientToApi.Chip, chip);
            request.addData(TransferNames.ClientToApi.Vendor, vendor);


            //process request & get the response as "raw" message
            var response = await processRequest(Consts.Client.GetReviewListApi, request, true);

            //get the domain list from response data
            var messageInXml = Utils.StringToXml(response.Message);
            var reviewList = messageInXml.Element(TransferNames.ApiToClient.ReviewList);


            //go through each review element and add it to return list
            var returnList = new List<Review>();
            foreach (var domain in reviewList.Elements())
            {
                //seperate the raw elements that make a Review
                //note: if null reference thrown here, than cause is XML file missing element
                var username = domain.Element(TransferNames.ApiToClient.Username).Value;
                var title = domain.Element(TransferNames.ApiToClient.Title).Value;
                var reviewText = domain.Element(TransferNames.ApiToClient.ReviewText).Value;
                var _chip = domain.Element(TransferNames.ApiToClient.Chip).Value;
                var _vendor = domain.Element(TransferNames.ApiToClient.Vendor).Value;
                var rating = int.Parse(domain.Element(TransferNames.ApiToClient.Rating).Value);
                var time = domain.Element(TransferNames.ApiToClient.Time).Value;


                //create a Review from raw data & add it to the list
                var parsedReview = new Review();
                parsedReview.Username = username;
                parsedReview.Title = title;
                parsedReview.ReviewText = reviewText;
                parsedReview.Chip = _chip;
                parsedReview.Vendor = _vendor;
                parsedReview.Rating = rating;
                parsedReview.Time = time;
                returnList.Add(parsedReview);
            }

            //return list to caller
            return returnList;
        }



        /** PRIVATE METHODS **/

        /// <summary>
        /// Sends request to API server & reports the status of the response to logger
        /// Returns the formatted response, for raw response (XML element) pass in true for raw.
        /// Note: raw is used when data inside Message element needs to be parsed differently
        /// </summary>
        private static async Task<Response> processRequest(string pathToApi, TransferData request, bool raw = false)
        {
            //send request to server and get the response
            var rawResponse = await SendRequest(pathToApi, request);

            //parse the response
            var parsedResponse = ParseResponse(raw, rawResponse);

            //log the response
            LogResponse(parsedResponse);

            //return it caller
            return parsedResponse;



            //-----------------------------FUNCTIONS---------------------------------
            
            //extracts the a raw data into a Response data structure
            Response ParseResponse(bool rawMessage, TransferData transferData)
            {
                //get the data from the API server's reply
                var status = transferData.getChildData<string>(TransferNames.ApiToClient.Status);

                //if "raw" has been specified then get the Message as XML element else get the value inside the Message element
                var message =
                    rawMessage
                        ? transferData.getChild(TransferNames.ApiToClient.Message).ToString()
                        : transferData.getChildData<string>(TransferNames.ApiToClient.Message);

                //package the data nicely and return to caller
                var response = new Response(status, message);
                return response;
            }

            //sends the request to the server, and gets the response
            async Task<TransferData> SendRequest(string s, TransferData request1)
            {
                TransferData response = null;

                try
                {
                    //send request to server to add the domain, 
                    response = await Transfer.sendHttpData(s, request1);
                }
                //no internet error
                catch (HttpRequestException)
                {
                    LogManager.Error(Error.NoInternet);
                }

                //server unavailable error, end here with error to user
                if (response.GetHttpStatus() == HttpStatusCode.NotFound)
                {
                    LogManager.Error(Error.ServerDown);
                }

                return response;
            }

            void LogResponse(Response response)
            {
                //report success & failure to log manager
                if (response.IsFail()) 
                { LogManager.Error(response.Message); }
                else 
                { LogManager.Debug(response.Message); }
            }
        }

        

        /** ARCHIVED CODE **/

        //private async Task<XElement> listDomainApi(string key1)
        //{
        //    //prepare to request for list
        //    var request = new TransferData();
        //    request.addData(TransferNames.ClientToApi.Key1, key1); //for validation

        //    //send request to server and get response
        //    var response = await Transfer.sendHttpData(Path.Client.ListDomainsApi, request);

        //    //todo no internet handling here
        //    //if reponse is null no internet, end here & let caller know
        //    //if (response == null) { raiseNoInternetWarning(); return; }

        //    //get the list from data
        //    var xmlResponseData = response.getDataAsXml();
        //    var info = xmlResponseData.Element(TransferNames.ApiToClient.Info);
        //    var domainList = info.Element(TransferNames.ApiToClient.DomainList);

        //    //var listInXml = XElement.Parse(listAsString);

        //    //return domain list to caller

        //    //throw new NotImplementedException();

        //    return domainList;
        //}

    }
}



//ARCHIVED CODE

//public async Task<Response> DeleteDomain(Domain domain, string key1)
//{
//    //package data to be sent to API server
//    var request = new TransferData();
//    request.addData(TransferNames.ClientToApi.Key1, key1);
//    //use special delete IP that the server recognizes as a delete command
//    request.addData(TransferNames.ClientToApi.IP, Consts.DeleteIP);
//    request.addData(TransferNames.ClientToApi.SubDomain, domain.SubDomain);
//    request.addData(TransferNames.ClientToApi.TopDomain, domain.TopDomain);

//    //process request & get response
//    var response = await processRequest(Path.Client.DeleteDomainApi, request);

//    return response;
//}

//public async Task<Response> AddDomain(string newSubDomain, string selectedDomain, string key1)
//{
//    //package data to be sent to API server
//    var request = new TransferData();
//    request.addData(TransferNames.ClientToApi.Key1, key1);
//    request.addData(TransferNames.ClientToApi.SubDomain, newSubDomain);
//    request.addData(TransferNames.ClientToApi.TopDomain, selectedDomain);

//    //process request & handle all related events
//    var response = await processRequest(Path.Client.AddDomainApi, request);

//    return response;
//}

///// <summary>
///// Gets lists all domains that are linked to the user account
///// </summary>
//public async Task<List<Domain>> GetDomainList(string key1)
//{
//    //package data to be sent to API server
//    var request = new TransferData();
//    request.addData(TransferNames.ClientToApi.Key1, key1); //for validation

//    //process request & get the response as "raw" message
//    var response = await processRequest(Path.Client.ListDomainsApi, request, true);

//    //get the domain list from response data
//    var messageInXml = Utils.StringToXml(response.Message) ;
//    var domainList = messageInXml.Element(TransferNames.ApiToClient.DomainList);


//    //go through each domain element and add it to return list
//    var returnList = new List<Domain>();
//    foreach (var domain in domainList.Elements())
//    {
//        //get sub domain
//        var subDomain = domain.Element(TransferNames.ApiToClient.SubDomain).Value;
//        //get top domain
//        var topDomain = domain.Element(TransferNames.ApiToClient.TopDomain).Value;
//        //add both to list
//        returnList.Add(new Domain(subDomain, topDomain));
//    }

//    //return list to caller
//    return returnList;
//}
/// <summary>
/// Sends new IP to DNS Server and return the response
/// </summary>
//public Response UpdateDomain(Domain domain, string newIp, string key1)
//{
//    prepared data to send to API server
//    var request = new TransferData();
//    request.addData(TransferNames.ClientToApi.IP, newIp);
//    request.addData(TransferNames.ClientToApi.TopDomain, domain.TopDomain);
//    request.addData(TransferNames.ClientToApi.SubDomain, domain.SubDomain);
//    request.addData(TransferNames.ClientToApi.Key1, key1); //for validation

//    process request &handle all related events
//   var response = processRequest(Consts.Client.UpdateDomainApi, request).Result;

//    return response;
//}
///// <summary>
///// Based on username & password gets key1 stored at server, null if no account found
///// Key1 is the representation of the account, all interaction to server needs key1
///// </summary>
//public async Task<string> GetKey1(string username, string password)
//{

//    //check with API server if user account exists 

//    //prepare the request
//    var request = new TransferData();
//    var key1 = Utils.GenerateKey1(username, password);
//    request.addData(TransferNames.ClientToApi.Key1, key1); //for validation

//    //send request to server and get response
//    //todo change to new process request method
//    var response = await Transfer.sendHttpData(Path.Client.CheckAccountApi, request);

//    //if account exist, return the key1
//    if (response.getChildData<string>(TransferNames.ApiToClient.Status) == "Pass")
//    {
//        return key1;
//    }
//    //else account doesn't exist, return null
//    else
//    {
//        return null;
//    }


//}
