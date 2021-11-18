
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace API
{
    /// <summary>
    /// Is in charge of things pretaining to all the user accounts,
    /// Encapsulates the AccountList XML file, the only one allowed to read & write to said file
    /// </summary>
    public class AccountManager
    {

        /** BACKING FIELDS **/
        private readonly Data _accountList;



        /** CTOR **/
        public AccountManager(Data accountList) => _accountList = accountList;



        /** PUBLIC METHODS **/

        /// <summary>
        /// Gets all the user accounts
        /// </summary>
        //public List<UserAccount> getAllAccounts()
        //{
        //    //create a list to place the accounts into
        //    var returnList = new List<UserAccount>();

        //    //get all user accounts into a list (raw XML)
        //    var allUsers = accountList.getAllRecords();

        //    //place each raw account into a UserAccount structure
        //    foreach (var user in allUsers)
        //    {
        //        returnList.
        //    }

        //}

        /// <summary>
        /// Gets a user account from the inputed key1, returns null if not found
        /// </summary>
        public UserAccount getAccount(string key1)
        {
            //get the raw account record (xml)
            var rawRecord = getAccountRecord(key1);

            //if the raw record exists return the structred user account
            if (rawRecord != null)
            {
                var id = rawRecord.Element(DataFiles.API.AccountList.ID).Value;
                var username = rawRecord.Element(DataFiles.API.AccountList.Username).Value;
                var email = rawRecord.Element(DataFiles.API.AccountList.Email).Value;
                var key2 = rawRecord.Element(DataFiles.API.AccountList.KEY2).Value;
                var _lock = rawRecord.Element(DataFiles.API.AccountList.LOCK).Value;
                var account = new UserAccount(id, username, email, key1, key2, _lock);
                return account;
            }
            //else return null,
            else
            {
                return null;
            }

        }

        /// <summary>
        /// Check if the inputed domain is in use by another account
        /// </summary>
        public bool isDomainInUse(string domain)
        {
            //get all domains in use
            var allDomains = getAllDomainsInUse();

            //search the list to for the requested domain
            var found = from rec in allDomains where rec.Value == domain select rec;

            //if domain found, let user know
            return found.Any() ? true : false;
        }

        /// <summary>
        /// Adds a domain to a user account
        /// </summary>
        public void addDomain(string key1, Domain newDomain)
        {
            //get the underlying record for the account
            var record = getAccountRecord(key1);

            ////format the domain to be stored
            //var formatedDomain = new XElement(DataFiles.API.AccountList.Domain, newDomain);

            //get the element that holds the domains registered to the account
            var domainListHolder = record.Element(DataFiles.API.AccountList.DomainList);

            //add the new domain into the list
            domainListHolder.Add(newDomain.toXml());

            //save the changes to the underlying file
            _accountList.updateUnderlyingFile();
        }

        /// <summary>
        /// Deletes domain from a user account
        /// </summary>
        public void deleteDomain(string key1, Domain domain)
        {
            //get the underlying record for the account
            var record = getAccountRecord(key1);

            //get the element that holds the domains registered to the account
            var domainListHolder = record.Element(DataFiles.API.AccountList.DomainList);

            //get the record that holds that domain
            var foundRecords = from domainRecord in domainListHolder.Elements()
                               where Domain.fromXml(domainRecord).Equals(domain)
                               select domainRecord;

            //delete the record for the domain
            foundRecords.Remove();

            //save the changes permenantly
            _accountList.updateUnderlyingFile();
        }

        public void createAccount(string username, string email, string key1)
        {
            //generate the needed data
            var id = getNewId();
            var key2 = getNewKey2();
            var _lock = getNewLock(key1, key2);

            //put together the new account record
            var newRecord = new XElement(DataFiles.API.AccountList.Record,
                            new XElement(DataFiles.API.AccountList.ID, id),
                            new XElement(DataFiles.API.AccountList.Username, username),
                            new XElement(DataFiles.API.AccountList.Email, email),
                            new XElement(DataFiles.API.AccountList.KEY2, key2),
                            new XElement(DataFiles.API.AccountList.LOCK, _lock),
                            //on creation account list will always be empty, domains are added seperately
                            new XElement(DataFiles.API.AccountList.DomainList)
                        );

            //place new record into main account list
            _accountList.insertRecord(newRecord);
        }

        public void deleteAccount(string key1)
        {
            //get the record (xml) that hold the account info
            var accountRecord = getAccountRecord(key1);

            //delete the record based on the account record
            _accountList.deleteRecord(accountRecord);

            //save the changes permenantly
            _accountList.updateUnderlyingFile();

        }

        /// <summary>
        /// Gets the list of domains associated with this account in XML format
        /// </summary>
        public XElement getDomainList(string key1)
        {
            //get the record (xml) that hold the account info
            var accountRecord = getAccountRecord(key1);

            //get raw domain list (XML)
            var domainList = accountRecord.Element(DataFiles.API.AccountList.DomainList);

            //convert xml list to string representation
            //var domainsString = TransferData.xmlToString(domainList);

            return domainList;
        }



        /** PRIVATE METHODS **/
        /// <summary>
        /// Gets the account record from main list based on key1, returns null if not found
        /// </summary>
        private XElement getAccountRecord(string key1)
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
        /// Returns a list accounts in their xml form (still linked to their root file)
        /// </summary>
        private IEnumerable<XElement> getAllAccountsAsXml() => _accountList.getAllRecords();

        /// <summary>
        /// checks if key1 can unlock key2 and lock
        /// </summary>
        private bool isKeyMatch(string key1, string key2, string originalLock)
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
        private List<XElement> getAllDomainsInUse()
        {
            //get all user accounts into a list (raw XML)
            var allUsers = _accountList.getAllRecords();

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

        /// <summary>
        /// Generates a new lock from the inputed keys
        /// Lock is the hash of key1 & key2 (SHA256)
        /// </summary>
        private string getNewLock(string key1, string key2) => Utils.StringToHash(key1 + key2);

        /// <summary>
        /// Generates a new key2 based on current server time
        /// time now > unix timestamp (ms) > sha256
        /// </summary>
        private string getNewKey2()
        {
            //get timestamp
            var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds().ToString();

            //get the hash of the timestamp (sha256)
            var sha256 = Utils.StringToHash(timestamp);

            //return the hash to the caller
            return sha256;
        }

        /// <summary>
        /// Generates a new ID for a new user, 
        /// based on already existing user records 
        /// </summary>
        private int getNewId()
        {
            //find the biggest id in the main account list 
            var idElement = DataFiles.API.AccountList.ID;

            int biggestId;

            try
            {
                biggestId = getAllAccounts().Max(el => int.Parse(el.Element(idElement).Value));
            }
            //if there is an error getting the biggest id, default to biggest ID 0
            //possible error when first account is created
            catch (Exception)
            {
                biggestId = 0;
            }

            //increament the id by 1 & return it to caller
            return biggestId + 1;
        }

        /// <summary>
        /// returns a list accounts in their xml form (still linked to their root file)
        /// </summary>
        private IEnumerable<XElement> getAllAccounts() => _accountList.getAllRecords();




    }
}
