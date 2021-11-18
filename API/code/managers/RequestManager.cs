
using BlazorApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace API
{
    /// <summary>
    /// Encapsulates the raw request coming from user
    /// </summary>
    public class RequestManager
    {
        private HttpRequestMessage _rawRequest;
        private TransferData _requestData;


        //shortcut for return codes
        private const HttpStatusCode Error = HttpStatusCode.InternalServerError;
        private const HttpStatusCode Ok = HttpStatusCode.OK;
        private const HttpStatusCode BadRequest = HttpStatusCode.BadRequest;


        public RequestManager(HttpRequestMessage request)
        {
            _rawRequest = request;

            //extract data from the raw request
            _requestData = new TransferData();
            _requestData.addData(_rawRequest);


        }

        /// <summary>
        /// Gets the preset response to send back to caller, based on inputed Reply option
        /// </summary>
        public HttpResponseMessage getReply(Reply option)
        {
            switch (option)
            {
                case Reply.ValidationFailed:
                    return _rawRequest.CreateResponse(Ok, Message.create("Fail", "Invalid username or password"));
                case Reply.SendingToDNSFailed:
                    return _rawRequest.CreateResponse(Ok, Message.create("Fail", "Failed to update DNS server"));
                case Reply.UpdatingCacheFailed:
                    return _rawRequest.CreateResponse(Ok, Message.create("Fail", "DNS updated, but cache update failed"));
                case Reply.DomainUpdated:
                    //use ip & domain found in request to update
                    return _rawRequest.CreateResponse(Ok, Message.create("Pass", $"{getTopDomain()} -> {getIpAddress()}"));
                case Reply.DomainNotAvailable:
                    return _rawRequest.CreateResponse(Ok, Message.create("Fail", "Domain not available"));
                case Reply.DomainAvailable:
                    return _rawRequest.CreateResponse(Ok, Message.create("Pass", "Domain available"));
                case Reply.DomainAdded:
                    return _rawRequest.CreateResponse(Ok, Message.create("Pass", "Domain added"));
                case Reply.DomainDeleted:
                    return _rawRequest.CreateResponse(Ok, Message.create("Pass", "Domain deleted"));
                case Reply.AccountExists:
                    return _rawRequest.CreateResponse(Ok, Message.create("Pass", "User exists"));
                case Reply.NoAccountExists:
                    return _rawRequest.CreateResponse(Ok, Message.create("Fail", "No such user exist"));
                case Reply.InvalidAccountData:
                    return _rawRequest.CreateResponse(Ok, Message.create("Fail", "Invalid data"));
                case Reply.AccountCreated:
                    return _rawRequest.CreateResponse(Ok, Message.create("Pass", "New user created"));
                case Reply.AccountNotCreated:
                    return _rawRequest.CreateResponse(Ok, Message.create("Fail", "User not created"));
                case Reply.AccountDeleted:
                    return _rawRequest.CreateResponse(Ok, Message.create("Pass", "User deleted"));
                case Reply.UnexpectedError:
                    return _rawRequest.CreateResponse(Ok, Message.create("Fail", "Unexpected error has occured"));
                default:
                    throw new Exception("Message for Reply option not found!");
            }

        }

        /// <summary>
        /// Gets the preset response to send back to caller, based on inputed Reply option
        /// but with added data from caller
        /// </summary>
        public HttpResponseMessage getReply(Reply option, string data)
        {
            switch (option)
            {
                case Reply.UnexpectedError:
                    return _rawRequest.CreateResponse(Ok, Message.create("Fail", data));
                case Reply.DomainNotAvailable:
                    return _rawRequest.CreateResponse(Ok, Message.create("Fail", data));
                default:
                    throw new Exception("Message for Reply option not found!");
            }

        }

        /// <summary>
        /// Gets the preset response to send back to caller, based on inputed Reply option
        /// but with added data from caller
        /// </summary>
        public HttpResponseMessage getReply(Reply option, XElement data)
        {
            switch (option)
            {
                case Reply.ReviewList:
                    return _rawRequest.CreateResponse(Ok, Message.create("Pass", data));
                default:
                    throw new Exception("Message for Reply option not found!");
            }

        }


        /// <summary>
        /// Gets the KEY1 found in the request data
        /// </summary>
        /// <returns></returns>
        //public string getKey1() => _requestData.getChildData<string>(TransferNames.ClientToApi.Key1);

        /// <summary>
        /// Gets the IP address found in the request data
        /// </summary>
        public string getIpAddress() => _requestData.getChildData<string>(TransferNames.ClientToApi.IP);

        /// <summary>
        /// Gets the domain found in the request data
        /// </summary>
        public string getTopDomain() => _requestData.getChildData<string>(TransferNames.ClientToApi.TopDomain);
        public string getSubDomain() => _requestData.getChildData<string>(TransferNames.ClientToApi.SubDomain);

        public string getUsername() => _requestData.getChildData<string>(TransferNames.ClientToApi.Username);

        public string getEmail() => _requestData.getChildData<string>(TransferNames.ClientToApi.Email);
        public string getChip() => _requestData.getChildData<string>(TransferNames.ClientToApi.Chip);
        public string getVendor() => _requestData.getChildData<string>(TransferNames.ClientToApi.Vendor);
    }
}
