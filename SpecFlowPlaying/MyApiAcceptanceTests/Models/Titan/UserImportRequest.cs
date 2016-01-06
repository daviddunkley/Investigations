using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiAcceptanceTests.Models.Titan
{
    public class UserImportRequest
    {
        public UserImportRequest()
        {
            Details = new Dictionary<string, string>();
        }

        public string EmailAddress { get; set; }
        public PublicationData Publication { get; set; }
        public Dictionary<string, string> Details { get; set; }

        public string FormId { get; set; }
        public string ClientId { get; set; }

        public Guid ProductId { get; set; }

    }
}
