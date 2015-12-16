using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using MyApiAcceptanceTests.Models;
using Newtonsoft.Json;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;

namespace MyApiAcceptanceTests
{
    [Binding]
    public class MyApiResponseSteps: ApiResponseSteps<UserRequest>
    {
        private readonly Fixture _fixture;
        private HttpClient _httpClient;
        private HttpResponseMessage _httpResponseMessage;
        private Uri _requestUri;

        public MyApiResponseSteps()
        {
            _fixture = new Fixture();

            RequestObject = new UserRequest
            {
                Publication = new Publication(),
                UserDetail = new UserDetail()
            };
        }

        private bool TryExtractEmailFieldFromTable(Table table, string fieldName, out string fieldValue, bool autoGenerate = false)
        {
            fieldValue = null;
            if (table.Rows.Any(r => r["Field"] == fieldName))
            {
                fieldValue = table.Rows.First(r => r["Field"] == fieldName)["Value"];

                if (autoGenerate)
                {
                    fieldValue = (fieldValue == "{unique}")
                        ? $"{_fixture.Create<string>()}@{_fixture.Create<string>()}.com"
                        : fieldValue;
                }
                return true;
            }

            return false;
        }

        private bool TryExtractFieldFromTable(Table table, string fieldName, out string fieldValue, bool autoGenerate = false)
        {
            fieldValue = null;
            if (table.Rows.Any(r => r["Field"] == fieldName))
            {
                fieldValue = table.Rows.First(r => r["Field"] == fieldName)["Value"];

                if (autoGenerate)
                {
                    fieldValue = (fieldValue == "{unique}")
                        ? $"{_fixture.Create<string>()}"
                        : fieldValue;
                }
                return true;
            }

            return false;
        }

        #region Given Steps
        [Given(@"a User Request of:")]
        public void GivenAUserRequestOf(Table table)
        {
            string fieldValue;

            if (TryExtractEmailFieldFromTable(table, "EmailAddress", out fieldValue, true))
            {
                RequestObject.EmailAddress = fieldValue;
            }

            if (TryExtractFieldFromTable(table, "ClientId", out fieldValue))
            {
                RequestObject.ClientId = fieldValue;
            }

            if (TryExtractFieldFromTable(table, "ProductId", out fieldValue))
            {
                Guid guid;
                if (Guid.TryParse(fieldValue, out guid))
                {
                    RequestObject.ProductId = guid;
                }
            }

            if (TryExtractFieldFromTable(table, "FormId", out fieldValue))
            {
                RequestObject.FormId = fieldValue;
            }
        }

        [Given(@"a Publication of:")]
        public void GivenAPublicationOf(Table table)
        {
            string fieldValue;

            if (TryExtractFieldFromTable(table, "AdditionalParameters", out fieldValue))
            {
                RequestObject.Publication.AdditionalParameters = fieldValue;
            }

            if (TryExtractFieldFromTable(table, "PublicationId", out fieldValue))
            {
                int id;
                if (int.TryParse(fieldValue, out id))
                {
                    RequestObject.Publication.PublicationId = id;
                }
            }

            if (TryExtractFieldFromTable(table, "StartDate", out fieldValue))
            {
                DateTime dt;
                if (DateTime.TryParse(fieldValue, out dt))
                {
                    RequestObject.Publication.StartDate = dt;
                }
            }

            if (TryExtractFieldFromTable(table, "SendEmail", out fieldValue))
            {
                bool yesNo;
                if (bool.TryParse(fieldValue, out yesNo))
                {
                    RequestObject.Publication.SendEmail = yesNo;
                }
            }
        }

        [Given(@"a UserDetails of:")]
        public void GivenAUserDetailsOf(Table table)
        {
            string fieldValue;

            if (TryExtractFieldFromTable(table, "Title", out fieldValue, true))
            {
                RequestObject.UserDetail.Title = fieldValue;
            }

            if (TryExtractFieldFromTable(table, "FirstName", out fieldValue, true))
            {
                RequestObject.UserDetail.FirstName = fieldValue;
            }

            if (TryExtractFieldFromTable(table, "LastName", out fieldValue, true))
            {
                RequestObject.UserDetail.LastName = fieldValue;
            }

            if (TryExtractFieldFromTable(table, "Company", out fieldValue, true))
            {
                RequestObject.UserDetail.Company = fieldValue;
            }

            if (TryExtractFieldFromTable(table, "Address1", out fieldValue, true))
            {
                RequestObject.UserDetail.Address1 = fieldValue;
            }

            if (TryExtractFieldFromTable(table, "Address2", out fieldValue, true))
            {
                RequestObject.UserDetail.Address2 = fieldValue;
            }
            if (TryExtractFieldFromTable(table, "Address3", out fieldValue, true))
            {
                RequestObject.UserDetail.Address3 = fieldValue;
            }
            if (TryExtractFieldFromTable(table, "Address4", out fieldValue, true))
            {
                RequestObject.UserDetail.Address4 = fieldValue;
            }
            if (TryExtractFieldFromTable(table, "Address5", out fieldValue, true))
            {
                RequestObject.UserDetail.Address5 = fieldValue;
            }
            if (TryExtractFieldFromTable(table, "City", out fieldValue, true))
            {
                RequestObject.UserDetail.City = fieldValue;
            }
            if (TryExtractFieldFromTable(table, "PostCode", out fieldValue, true))
            {
                RequestObject.UserDetail.PostCode = fieldValue;
            }
            if (TryExtractFieldFromTable(table, "Country", out fieldValue, true))
            {
                RequestObject.UserDetail.Country = fieldValue;
            }
        }

        #endregion Given Steps
    }
}
