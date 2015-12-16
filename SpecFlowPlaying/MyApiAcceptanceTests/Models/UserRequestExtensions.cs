using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiAcceptanceTests.Models
{
    public static class UserRequestExtensions
    {
        public static void Merge(this UserRequest instanceA, UserRequest instanceB)
        {
            if (instanceA == null || instanceB == null)
            {
                return;
            }

            if (instanceB.EmailAddress != null)
            {
                instanceA.EmailAddress = instanceB.EmailAddress;
            }

            if (instanceB.ClientId != null)
            {
                instanceA.ClientId = instanceB.ClientId;
            }

            if (instanceB.ProductId != null)
            {
                instanceA.ProductId = instanceB.ProductId;
            }

            if (instanceB.FormId != null)
            {
                instanceA.FormId = instanceB.FormId;
            }

        }
    }
}
