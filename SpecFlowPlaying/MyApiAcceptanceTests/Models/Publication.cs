using System;

namespace MyApiAcceptanceTests.Models
{
    public class Publication
    {
        public int PublicationId { get; set; }
        public string Name { get; set; }
        public Uri Url { get; set; }
        public DateTime StartDate { get; set; }
        public Uri ContentViewed { get; set; }
        public string AdditionalParameters { get; set; }
    }
}
