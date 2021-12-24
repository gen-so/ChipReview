
using BlazorApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace API
{
    /// <summary>
    /// Is in charge of things pretaining to all the reviews,
    /// Encapsulates the ReviewList XML file, the only one allowed to read & write to said file
    /// </summary>
    public static class ReviewManager
    {

        /** BACKING FIELDS **/
        private static Data _reviewList;



        /** CTOR **/
        public static void Initialize(Data reviewList) => _reviewList = reviewList;



        /** PUBLIC METHODS **/

        /// <summary>
        /// Gets review list based on chip & vendor, returns in xml format
        /// </summary>
        public static XElement getReviewList(string chip, string vendor)
        {

            //filter review list by chip & vendor
            var found =
                from record in getAllReviewsAsXml()
                where
                    record.Element(DataFiles.API.ReviewList.Chip)?.Value == chip &&
                    record.Element(DataFiles.API.ReviewList.Vendor)?.Value == vendor
                select record;

            //place list inside a xml element
            var reviewList = new XElement("ReviewList");
            reviewList.Add(found);

            return reviewList;
        }

        /// <summary>
        /// Gets all reviews, debug purposes
        /// </summary>
        public static XElement getReviewListAll()
        {

            //filter review list by chip & vendor
            var found =
                from record in getAllReviewsAsXml()
                select record;

            //place list inside a xml element
            var reviewList = new XElement("ReviewList");
            reviewList.Add(found);

            return reviewList;
        }

        /// <summary>
        /// Adds a new review
        /// </summary>
        public static void addReview(Review newReview)
        {
            //get the underlying record for the account
            //var record = getAccountRecord(key1);

            ////format the domain to be stored
            //var formatedDomain = new XElement(DataFiles.API.AccountList.Domain, newDomain);

            //get the element that holds the domains registered to the account
            //var domainListHolder = record.Element(DataFiles.API.AccountList.DomainList);

            //add the new review into the XML list
            var reviewXml = newReview.ToXml();
            _reviewList.insertRecord(reviewXml);

            //save the changes to the underlying file
            //_reviewList.updateUnderlyingFile();
        }

        public static void deleteReview(string reviewHash)
        {
            //get the record (xml) that holds the review info
            var reviewRecord = getReviewRecord(reviewHash);

            //delete the reviews found
            _reviewList.deleteRecord(reviewRecord);

            //save the changes permanently
            _reviewList.updateUnderlyingFile();
        }



        /** PRIVATE METHODS **/



        /// <summary>
        /// Gets the account record from main list based on key1, returns null if not found
        /// </summary>
        private static XElement getAccountRecord(string key1)
        {
            //get only the record which key1 can unlock
            var found =
                from record in getAllAccountsAsXml()
                where isKeyMatch(
                    key1,
                    record.Element(DataFiles.API.AccountList.KEY2)?.Value,
                    record.Element(DataFiles.API.AccountList.LOCK)?.Value)
                select record;

            return found.FirstOrDefault();
        }

        /// <summary>
        /// Gets review record by hash, returned in as XML
        /// </summary>
        private static XElement getReviewRecord(string reviewHash)
        {
            //get only the record which hash match
            var found =
                from record in getAllReviewsAsXml()
                where
                    record.Element(DataFiles.API.ReviewList.Hash)?.Value == reviewHash
                select record;



            return found.FirstOrDefault();
        }

        /// <summary>
        /// Returns a list accounts in their xml form (still linked to their root file)
        /// </summary>
        private static IEnumerable<XElement> getAllAccountsAsXml() => _reviewList.getAllRecords();

        /// <summary>
        /// Returns a list of reviews in their xml form (still linked to their root file)
        /// </summary>
        private static IEnumerable<XElement> getAllReviewsAsXml() => _reviewList.getAllRecords();

        /// <summary>
        /// checks if key1 can unlock key2 and lock
        /// </summary>
        private static bool isKeyMatch(string key1, string key2, string originalLock)
        {
            //combine both keys
            var combinedKey = key1 + key2;

            //get hash of combined key
            var possibleLock = Utils.StringToHash(combinedKey);

            //if lock matches, validation has passed, key is valid
            return (possibleLock == originalLock) ? true : false;
        }

        /// <summary>
        /// Gets all the domains in use by users, returns in XML
        /// </summary>
        private static List<XElement> getAllDomainsInUse()
        {
            //get all user accounts into a list (raw XML)
            var allUsers = _reviewList.getAllRecords();

            //a place to store the domain list, for returning
            var returnList = new List<XElement>();

            //go through each user and get the domains in use by them
            foreach (var user in allUsers)
            {
                //get raw domain list parent element (XML)
                var domainListHolder = user.Element(DataFiles.API.AccountList.DomainList);
                //get the individual domain elements
                var domainList = domainListHolder.Elements();
                //add all the domains into the main list
                returnList.AddRange(domainList);
            }

            //return main list to caller
            return returnList;
        }



    }
}
