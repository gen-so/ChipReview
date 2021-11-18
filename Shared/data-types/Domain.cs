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
        public string ReviewText { get; set; }
        public string Rating { get; set; }




        /// <summary>
        /// Gets the Review in XML form
        /// </summary>
        public XElement toXml()
        {
            throw new NotImplementedException();
            //var subReview = new XElement(DataFiles.Client.Config.SubReview, SubReview);
            //var topReview = new XElement(DataFiles.Client.Config.TopReview, TopReview);
            //var fullReview = new XElement(DataFiles.Client.Config.Review, subReview, topReview);

            ////return the full Review in xml
            //return fullReview;
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