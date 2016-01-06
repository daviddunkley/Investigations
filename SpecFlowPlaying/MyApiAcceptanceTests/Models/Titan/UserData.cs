using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiAcceptanceTests.Models.Titan
{
    public class UserData
    {
        public string EmailAddress { get; set; }
        public Dictionary<string, string> Details { get; set; }

        public UserData()
        {
            Details = new Dictionary<string, string>();
        }
    }
}
