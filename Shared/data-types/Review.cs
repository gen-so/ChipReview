using System;
using System.Xml.Linq;

namespace BlazorApp.Shared
{

    /// <summary>
    /// A data representation of a Review
    /// </summary>
    public class Review
    {


        public string Username { get; set; }
        public string Chip { get; set; }
        public string Vendor { get; set; }
        public string Time { get; set; }
        public string Title { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }


        /// <summary>
        /// CTOR Used to set default values the review items
        /// </summary>
        public Review()
        {
            Username = "Anonymous";
            Title = "Best Review Ever!";
            ReviewText = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam";
            Rating = 5;
        }



        /// <summary>
        /// Gets the Review in XML form
        /// Used when transferring & storage
        /// </summary>
        public XElement ToXml()
        {
            //construct an XML version of the Review object
            var chip = new XElement(DataFiles.API.ReviewList.Chip, Chip);
            var vendor = new XElement(DataFiles.API.ReviewList.Vendor, Vendor);
            var username = new XElement(DataFiles.API.ReviewList.Username, Username);
            var rating = new XElement(DataFiles.API.ReviewList.Rating, Rating);
            var title = new XElement(DataFiles.API.ReviewList.Title, Title);
            var time = new XElement(DataFiles.API.ReviewList.Time, Time);
            var reviewText = new XElement(DataFiles.API.ReviewList.ReviewText, ReviewText);
            var fullReview = new XElement(DataFiles.API.ReviewList.Review, chip, vendor, username, rating, title, time, reviewText);

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
            var hash1 = Username.GetHashCode();
            var hash2 = Chip.GetHashCode();
            var hash3 = Vendor.GetHashCode();
            var hash4 = Time.GetHashCode();

            return hash1 + hash2 + hash3 + hash4;
        }

        public override string ToString()
        {
            return $"{Username} - {Chip} - {Vendor} - {Time}";
        }
    }
}