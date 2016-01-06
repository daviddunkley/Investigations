using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiAcceptanceTests.Models.Titan
{
    public class UserImportResponse
    {
        public UserData UserData { get; set; }
        public bool CreatedUser { get; set; }
        public Uri VerifyUrl { get; set; }
    }
}
