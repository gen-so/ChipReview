using System;
using BlazorApp.Shared;
using System.Net.Http;

namespace API
{

    /// <summary>
    /// This is where actual API logic is.
    /// EntryPoint sends execution here
    /// </summary>
    public static class API
    {

        //place to store extra info data, for sending to user
        private static string extraInfo = "";




        /** PUBLIC METHODS **/


        public static HttpResponseMessage getReview()
        {
            //if the account used to make the request doesn't exist, end here with error
            //if (_userAccount == null) { return RequestManager.getReply(Reply.ValidationFailed); }

            //get list of reviews based on chip & vendor received in query
            var chip = RequestManager.getChip();
            var vendor = RequestManager.getVendor();
            var reviewList = ReviewManager.getReviewList(chip, vendor);

            //get list of domains associated with this user
            //var domainList = _accountManager.getDomainList(RequestManager.getKey1());

            //return this list to client
            return RequestManager.getReply(Reply.ReviewList, reviewList);
        }
        
        public static HttpResponseMessage getReviewAll()
        {
            //if the account used to make the request doesn't exist, end here with error
            //if (_userAccount == null) { return RequestManager.getReply(Reply.ValidationFailed); }

            //get list of reviews based on chip & vendor received in query
            //var chip = RequestManager.getChip();
            //var vendor = RequestManager.getVendor();
            var reviewList = ReviewManager.getReviewListAll();

            //get list of domains associated with this user
            //var domainList = _accountManager.getDomainList(RequestManager.getKey1());

            //return this list to client
            return RequestManager.getReply(Reply.ReviewList, reviewList);
        }
        
        public static HttpResponseMessage getChipAll()
        {
            //if the account used to make the request doesn't exist, end here with error
            //if (_userAccount == null) { return RequestManager.getReply(Reply.ValidationFailed); }

            //get list of reviews based on chip & vendor received in query
            var chipList = ChipManager.getChipListAll();


            //return this list to client
            return RequestManager.getReply(Reply.ChipList, chipList);
        }
       
        public static HttpResponseMessage deleteReview()
        {
            var result = true; //default is pass

            try
            {
                //get the hash of the review to be deleted
                var reviewHash = RequestManager.getReviewHash();

                //delete the review with matching hash
                ReviewManager.deleteReview(reviewHash);

            }
            //any errors mark as failed
            catch { result = false; }

            //return pass or fail result to caller
            return result ? RequestManager.getReply(Reply.ReviewDeleted) : RequestManager.getReply(Reply.ReviewNotDeleted);
        }

        public static HttpResponseMessage addNewReview()
        {
            //if the account used to make the request doesn't exist, end here with error
            //if (_userAccount == null) { return RequestManager.getReply(Reply.ValidationFailed); }

            //if available full domain add to account
            var newReview = new Review();
            newReview.Chip = RequestManager.getChip();
            newReview.Vendor = RequestManager.getVendor();
            newReview.Username = RequestManager.getUsername();
            newReview.Rating = RequestManager.getRating();
            newReview.Title = RequestManager.getTitle();
            newReview.Time = RequestManager.getTime();
            newReview.ReviewText = RequestManager.getReviewText();

            ReviewManager.addReview(newReview);

            //let user know domain has been added to account
            return RequestManager.getReply(Reply.NewReviewAdded);
        }

        



        /** PRIVATE METHODS **/

        /// <summary>
        /// Adds extra info to be sent in reply to user
        /// This function makes sure extra info is formatted properly
        /// </summary>
        private static void addExtraInfo(string info)
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

    }
}
