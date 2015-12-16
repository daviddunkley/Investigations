using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiAcceptanceTests.Models
{
    public static class UserDetailExtensions
    {
        public static void Merge(this UserDetail instanceA, UserDetail instanceB)
        {
            if (instanceA == null || instanceB == null)
            {
                return;
            }

            if (instanceB.Title != null)
            {
                instanceA.Title = instanceB.Title;
            }

            if (instanceB.FirstName != null)
            {
                instanceA.FirstName = instanceB.FirstName;
            }

            if (instanceB.LastName != null)
            {
                instanceA.LastName = instanceB.LastName;
            }

            if (instanceB.Company != null)
            {
                instanceA.Company = instanceB.Company;
            }

            if (instanceB.Address1 != null)
            {
                instanceA.Address1 = instanceB.Address1;
            }

            if (instanceB.Address2 != null)
            {
                instanceA.Address2 = instanceB.Address2;
            }

            if (instanceB.Address3 != null)
            {
                instanceA.Address3 = instanceB.Address3;
            }

            if (instanceB.Address4 != null)
            {
                instanceA.Address4 = instanceB.Address4;
            }

            if (instanceB.Address5 != null)
            {
                instanceA.Address5 = instanceB.Address5;
            }

            if (instanceB.City != null)
            {
                instanceA.City = instanceB.City;
            }

            if (instanceB.PostCode != null)
            {
                instanceA.PostCode = instanceB.PostCode;
            }

            if (instanceB.Country != null)
            {
                instanceA.Country = instanceB.Country;
            }
        }
    }
}
