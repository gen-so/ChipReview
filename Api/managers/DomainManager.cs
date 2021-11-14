
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlazorApp.Api
{
    /// <summary>
    /// Represents everthing that has to do with the domains
    /// </summary>
    public class DomainManager
    {
        private readonly Data _domainList;
        private readonly Data _domainLog;

        /** BACKING FEILDS **/


        /**  CTOR **/
        public DomainManager(Data domainList, Data domainLog)
        {
            _domainList = domainList;
            _domainLog = domainLog;
        }



        /** PUBLIC METHODS **/

        //updates the local domains to ip address list
        //a copy of the dns records, for faster retrival
        public bool updateCache(string ipAddress, string domain)
        {
            try
            {
                //prepare new record to be saved
                XElement newRecord = new XElement(DataFiles.API.DomainList.Record,
                    new XElement(DataFiles.API.DomainList.Domain, domain),
                    new XElement(DataFiles.API.DomainList.IP, ipAddress),
                    new XElement(DataFiles.API.DomainList.Time, DateTime.UtcNow)
                );

                //if record with same domain name already exist, move that record to log list first
                if (_domainList.isExist(DataFiles.API.DomainList.Domain, domain)) { moveRecordToLog(domain); }

                //add new record to the document
                _domainList.insertRecord(newRecord);

                //if all went well, return true
                return true;

            }
            catch (Exception)
            {
                //if failure occurs, return false
                return false;
            }

        }





        /** PRIVATE METHODS **/
        /// <summary>
        /// moves record with matching domain name to log list
        /// </summary>
        private void moveRecordToLog(string domain)
        {
            //get the record that already exist
            var record = _domainList.getRecord(DataFiles.API.DomainList.Domain, domain);

            //insert record into ip log list
            _domainLog.insertRecord(record);

            //delete the record from ip list
            _domainList.deleteRecord(record);

        }

    }
}
