using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApiAcceptanceTests.Models;

namespace MyApiAcceptanceTests
{
    public class UserRequest
    {
        public string EmailAddress { get; set; }
        public Publication Publication { get; set; }
        public UserDetail UserDetail { get; set; }
        public string FormId { get; set; }
        public string ClientId { get; set; }
        public Guid? ProductId { get; set; }
    }
}
