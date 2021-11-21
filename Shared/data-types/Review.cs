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


        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
            //return (obj as Review)?.FullReview == this.FullReview;
        }

        //todo override hashcode
    }
}