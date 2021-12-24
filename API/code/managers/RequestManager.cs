
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
    public static class RequestManager
    {
        private static HttpRequestMessage _rawRequest;
        private static TransferData _requestData;


        //shortcut for return codes
        private const HttpStatusCode Error = HttpStatusCode.InternalServerError;
        private const HttpStatusCode Ok = HttpStatusCode.OK;
        private const HttpStatusCode BadRequest = HttpStatusCode.BadRequest;


        /// <summary>
        /// Initializes the request manager with a request.
        /// The first thing to do before using the manager
        /// </summary>
        public static void Initialize(HttpRequestMessage request)
        {
            _rawRequest = request;

            //extract data from the raw request
            _requestData = new TransferData();
            _requestData.addData(_rawRequest);


        }

        /// <summary>
        /// Gets the preset response to send back to caller, based on inputed Reply option
        /// </summary>
        public static HttpResponseMessage getReply(Reply option)
        {
            switch (option)
            {
                case Reply.ValidationFailed:
                    return _rawRequest.CreateResponse(Ok, Message.create("Fail", "Invalid username or password"));
                case Reply.SendingToDNSFailed:
                    return _rawRequest.CreateResponse(Ok, Message.create("Fail", "Failed to update DNS server"));
                case Reply.UpdatingCacheFailed:
                    return _rawRequest.CreateResponse(Ok, Message.create("Fail", "DNS updated, but cache update failed"));
                case Reply.DomainNotAvailable:
                    return _rawRequest.CreateResponse(Ok, Message.create("Fail", "Domain not available"));
                case Reply.DomainAvailable:
                    return _rawRequest.CreateResponse(Ok, Message.create("Pass", "Domain available"));
                case Reply.NewReviewAdded:
                    return _rawRequest.CreateResponse(Ok, Message.create("Pass", "New review added, thank you!"));
                case Reply.ReviewDeleted:
                    return _rawRequest.CreateResponse(Ok, Message.create("Pass", "Review deleted"));
                case Reply.AccountExists:
                    return _rawRequest.CreateResponse(Ok, Message.create("Pass", "User exists"));
                case Reply.NoAccountExists:
                    return _rawRequest.CreateResponse(Ok, Message.create("Fail", "No such user exist"));
                case Reply.InvalidAccountData:
                    return _rawRequest.CreateResponse(Ok, Message.create("Fail", "Invalid data"));
                case Reply.AccountCreated:
                    return _rawRequest.CreateResponse(Ok, Message.create("Pass", "New user created"));
                case Reply.ReviewNotDeleted:
                    return _rawRequest.CreateResponse(Ok, Message.create("Fail", "Review not deleted"));
                case Reply.AccountDeleted:
                    return _rawRequest.CreateResponse(Ok, Message.create("Pass", "User deleted"));
                case Reply.UnexpectedError:
                    return _rawRequest.CreateResponse(Ok, Message.create("Fail", "Oops! Unexpected error has occurred"));
                default:
                    throw new Exception("Message for Reply option not found!");
            }

        }

        /// <summary>
        /// Gets the preset response to send back to caller, based on inputed Reply option
        /// but with added data from caller
        /// </summary>
        public static HttpResponseMessage getReply(Reply option, string data)
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
        /// NOTE: if adding an option, remember to case it here
        /// </summary>
        public static HttpResponseMessage getReply(Reply option, XElement data)
        {
            switch (option)
            {
                case Reply.ReviewList:
                    return _rawRequest.CreateResponse(Ok, Message.create("Pass", data));
                case Reply.ChipList:
                    return _rawRequest.CreateResponse(Ok, Message.create("Pass", data));
                default:
                    throw new Exception("Message for Reply option not found!");
            }

        }


        public static string getUsername() => _requestData.getChildData<string>(TransferNames.ClientToApi.Username);
        public static string getEmail() => _requestData.getChildData<string>(TransferNames.ClientToApi.Email);
        public static string getChip() => _requestData.getChildData<string>(TransferNames.ClientToApi.Chip);
        public static string getVendor() => _requestData.getChildData<string>(TransferNames.ClientToApi.Vendor);
        public static int getRating() => _requestData.getChildData<int>(TransferNames.ClientToApi.Rating);
        public static string getTitle() => _requestData.getChildData<string>(TransferNames.ClientToApi.Title);
        public static string getTime() => _requestData.getChildData<string>(TransferNames.ClientToApi.Time);
        public static string getReviewText() => _requestData.getChildData<string>(TransferNames.ClientToApi.ReviewText);
        public static string getReviewHash() => _requestData.getChildData<string>(TransferNames.ClientToApi.ReviewHash);
    }
}
