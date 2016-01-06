using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiAcceptanceTests.Models.Titan
{
    public class PublicationData
    {
        public PublicationData()
        {
            SendEmail = true;
        }

        public int PublicationId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime StartDate { get; set; }
        public string ContentViewed { get; set; }
        public string AdditionalParameters { get; set; }
        public bool SendEmail { get; set; }
    }
}
