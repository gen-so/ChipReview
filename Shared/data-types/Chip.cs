using System;
using System.Xml.Linq;

namespace BlazorApp.Shared
{

    /// <summary>
    /// A data representation of a Chip
    /// </summary>
    public class Chip
    {


        public string Model { get; set; }
        public string Vendor { get; set; }
        public int TotalRating { get; set; }
        public int ReviewCount { get; set; }
        public string ReleaseDate { get; set; }


        /// <summary>
        /// CTOR Used to set default values the review items
        /// </summary>
        public Chip()
        {
            //Username = "Anonymous";
            //Title = "Best Review Ever!";
            //ReviewText = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam";
            //Rating = 5;
        }



        /// <summary>
        /// Gets the Chip in XML form
        /// Used when transferring & storage
        /// </summary>
        public XElement ToXml()
        {
            //construct an XML version of the Chip object
            var model = new XElement(DataFiles.API.ChipList.Model, Model);
            var vendor = new XElement(DataFiles.API.ChipList.Vendor, Vendor);
            var totalRating = new XElement(DataFiles.API.ChipList.TotalRating, TotalRating);
            var reviewCount = new XElement(DataFiles.API.ChipList.ReviewCount, ReviewCount);
            var releaseDate = new XElement(DataFiles.API.ChipList.ReleaseDate, ReleaseDate);
            var fullReview = new XElement(DataFiles.API.ChipList.Chip, model, vendor, totalRating, reviewCount, releaseDate);

            //return the full Review in xml
            return fullReview;
        }


        /// <summary>
        /// Gets a new instance of Review from XML representation of it
        /// Needed when getting data from Server
        /// </summary>
        public static Review fromXml(XElement ReviewXml)
        {
            throw new NotImplementedException();

            //get sub Review & top Review from XML 
            //var subReview = ReviewXml.Element(DataFiles.Client.Config.SubReview).Value;
            //var topReview = ReviewXml.Element(DataFiles.Client.Config.TopReview).Value;

            //return new Review(subReview, topReview);
        }




        /** METHOD OVERRIDES **/
        public override bool Equals(object value)
        {

            if (value.GetType() == typeof(Review))
            {
                //cast to type
                var parsedValue = (Review)value;

                //check equality
                bool returnValue = (this.GetHashCode() == parsedValue.GetHashCode());

                return returnValue;
            }
            else
            {
                //Return false if value is null
                return false;
            }


        }

        public override int GetHashCode()
        {
            //get hash of all the fields & combine them
            //title & text not included because they can change
            var hash1 = Model.GetHashCode();
            var hash2 = Vendor.GetHashCode();

            return hash1 + hash2;
        }

        public override string ToString()
        {
            return $"{Model} - {Vendor}";
        }
    }
}