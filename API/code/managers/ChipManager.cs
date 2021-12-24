
using BlazorApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace API
{
    /// <summary>
    /// Is in charge of things pertaining to all the chips,
    /// Encapsulates the ChipList XML file, the only one allowed to read & write to said file
    /// </summary>
    public static class ChipManager
    {

        /** BACKING FIELDS **/
        private static Data _chipList;



        /** CTOR **/
        public static void Initialize(Data chipList) => _chipList = chipList;



        /** PUBLIC METHODS **/


        /// <summary>
        /// Gets all chips, debug purposes
        /// </summary>
        public static XElement getChipListAll()
        {

            //filter review list by chip & vendor
            var found =
                from record in getAllChipsAsXml()
                select record;

            //place list inside a xml element
            var reviewList = new XElement("ChipList");
            reviewList.Add(found);

            return reviewList;
        }


        /** PRIVATE METHODS **/

        /// <summary>
        /// Returns a list of reviews in their xml form (still linked to their root file)
        /// </summary>
        private static IEnumerable<XElement> getAllChipsAsXml() => _chipList.getAllRecords();



    }
}
