using BlazorApp.Shared;
using System.Net.Http;

namespace API
{

    /// <summary>
    /// This is where actual API logic is.
    /// EntryPoint sends execution here
    /// </summary>
    public class API
    {

        //manager for processing data
        private readonly RequestManager _requestManager;
        private readonly ApiConfigManager _configManager;
        private readonly AccountManager _accountManager;
        private readonly ReviewManager _reviewManager;
        //private readonly LogManager _logManager;
        private UserAccount _userAccount; //not readonly because can be assigned during account creation

        //place to store extra info data, for sending to user
        private string extraInfo = "";



        /**  CTOR **/
        /// <summary>
        /// Does all the prep work needed for the actual API functions to work
        /// </summary>
        public API(RequestManager requestManager, ApiConfigManager configManager, AccountManager accountManager, ReviewManager reviewManager)
        {
            //save the data managers
            _requestManager = requestManager;
            _configManager = configManager;
            _accountManager = accountManager;
            _reviewManager = reviewManager;
            //_domainManager = domainManager;
            //_logManager = logManager;

            //get the user account from the key1 found in the request
            //_userAccount = _accountManager.getAccount(requestManager.getKey1());
        }



        /** PUBLIC METHODS **/


        public HttpResponseMessage getReview()
        {
            //if the account used to make the request doesn't exist, end here with error
            //if (_userAccount == null) { return _requestManager.getReply(Reply.ValidationFailed); }

            //get list of reviews based on chip & vendor received in query
            var chip = _requestManager.getChip();
            var vendor = _requestManager.getVendor();
            var reviewList = _reviewManager.getReviewList(chip, vendor);

            //get list of domains associated with this user
            //var domainList = _accountManager.getDomainList(_requestManager.getKey1());

            //return this list to client
            return _requestManager.getReply(Reply.ReviewList, reviewList);
        }



        //public HttpResponseMessage updateDomain()
        //{
        //    //STEP 1:
        //    //if the account used to make the request doesn't exist, end here with error
        //    if (_userAccount == null) { return _requestManager.getReply(Reply.ValidationFailed); }


        //    //STEP 2:
        //    //update DNS server with incoming domain record
        //    //if sending to DNS failed, stop here & return error message to caller
        //    if (updateDnsRecord() == false) { return _requestManager.getReply(Reply.SendingToDNSFailed); }


        //    //STEP 3:
        //    //update local domain record cache
        //    //if updating cache failed, stop here & return error message to caller
        //    //get ip & domain to update from the request
        //    var ipAddress = _requestManager.getIpAddress();
        //    var domain = _requestManager.getTopDomain();
        //    if (_domainManager.updateCache(ipAddress, domain) == false) { return _requestManager.getReply(Reply.UpdatingCacheFailed); }


        //    //if control gets here, let user know all went well
        //    return _requestManager.getReply(Reply.DomainUpdated);
        //}

        //public HttpResponseMessage listDomain()
        //{
        //    //if the account used to make the request doesn't exist, end here with error
        //    if (_userAccount == null) { return _requestManager.getReply(Reply.ValidationFailed); }

        //    //get list of domains associated with this user
        //    //var domainList = _accountManager.getDomainList(_requestManager.getKey1());

        //    //return this list to client
        //    //return _requestManager.getReply(Reply.DomainList, domainList);
        //}

        public HttpResponseMessage addNewReview()
        {
            //if the account used to make the request doesn't exist, end here with error
            //if (_userAccount == null) { return _requestManager.getReply(Reply.ValidationFailed); }

            //if available full domain add to account
            var newReview = new Review();
            newReview.Chip = _requestManager.getChip();
            newReview.Vendor = _requestManager.getVendor();
            newReview.Username = _requestManager.getUsername();
            newReview.Rating = _requestManager.getRating();
            newReview.Title = _requestManager.getTitle();
            newReview.Time = _requestManager.getTime();
            newReview.ReviewText = _requestManager.getReviewText();

            _reviewManager.addReview(newReview);

            //let user know domain has been added to account
            return _requestManager.getReply(Reply.NewReviewAdded);
        }


        //checks the availability of a domain
        public HttpResponseMessage domainAvailability()
        {
            //if the account used to make the request doesn't exist, end here with error
            if (_userAccount == null) { return _requestManager.getReply(Reply.ValidationFailed); }

            //check if domain is available
            var domainIsAvailable = isDomainAvailable();

            //if NOT available, return here & tell user domain not available 
            if (!domainIsAvailable) { return _requestManager.getReply(Reply.DomainNotAvailable); }

            //if control reaches here, domain is available, let user know
            return _requestManager.getReply(Reply.DomainAvailable);
        }

        //checks if an account exists
        public HttpResponseMessage checkAccount()
        {
            //check if account exist and return results to caller
            if (_userAccount != null)
            {
                return _requestManager.getReply(Reply.AccountExists);
            }
            else
            {
                return _requestManager.getReply(Reply.NoAccountExists);
            }
        }

        //public HttpResponseMessage createAccount()
        //{
        //    //get the data needed to make an account
        //    var username = _requestManager.getUsername();
        //    var email = _requestManager.getEmail();
        //    //var key1 = _requestManager.getKey1();

        //    //if new account data is NOT valid, end here & let user know
        //    if (!newAccountDataIsValid(username, email, key1))
        //    {
        //        _requestManager.getReply(Reply.InvalidAccountData);
        //    }

        //    //create the account
        //    _accountManager.createAccount(username, email, key1);

        //    //get the newly created account
        //    _userAccount = _accountManager.getAccount(key1);

        //    //check if new account exist and return results to caller
        //    if (_userAccount != null)
        //    {
        //        return _requestManager.getReply(Reply.AccountCreated);
        //    }
        //    else
        //    {
        //        return _requestManager.getReply(Reply.AccountNotCreated);
        //    }
        //}

        //public HttpResponseMessage deleteAccount()
        //{
        //    //get the data needed delete an account
        //    var key1 = _requestManager.getKey1();

        //    //delete the account
        //    _accountManager.deleteAccount(key1);

        //    //let user know
        //    return _requestManager.getReply(Reply.AccountDeleted);

        //}




        /** PRIVATE METHODS **/



        /// <summary>
        /// Runs several checks on the inputed domain to see if it is available for adding into an account
        /// </summary>
        private bool isDomainAvailable()
        {
            //get the domain which needs to be checked
            var subDomain = _requestManager.getSubDomain();
            var topDomain = _requestManager.getTopDomain();
            var fullDomain = subDomain + topDomain;

            //CHECK 1 :
            //check if domain is one of the available top domains (part of configuration)
            var isTop = _configManager.isDomainInTopList(topDomain);

            //if domain is not in top list, note it in extra info
            if (isTop == false) { addExtraInfo("Root domain is not supported"); }


            //CHECK 2 :
            //check if another user already has the domain under their account
            var notInUse = !(_accountManager.isDomainInUse(fullDomain)); //bool fliped
            //if domain is already used by another user, note it in extra info
            if (notInUse == false) { addExtraInfo("Domain already in use"); }

            //if domain is in top list AND not in use by another account, domain is available
            return isTop && notInUse ? true : false;

        }

        /// <summary>
        /// Adds extra info to be sent in reply to user
        /// This function makes sure extra info is formated properly
        /// </summary>
        private void addExtraInfo(string info)
        {
            //if extra info already has data in it
            if (extraInfo != "")
            {
                //then add the new data as a new line
                extraInfo += "\n" + info;
            }
            //if this is the first data to be added
            else
            {
                //then add directly
                extraInfo = info;
            }
        }

        //WIP
        private bool newAccountDataIsValid(object username, object email, object key1)
        {
            //TODO new account validator
            return true;
        }

    }
}
