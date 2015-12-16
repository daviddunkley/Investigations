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
using TechTalk.SpecFlow.Assist;

namespace MyApiAcceptanceTests
{
    [Binding]
    public class MyApiResponseSteps : ApiResponseSteps<UserRequest>
    {
        private readonly Fixture _fixture;
        private HttpClient _httpClient;
        private HttpResponseMessage _httpResponseMessage;
        private Uri _requestUri;

        public MyApiResponseSteps()
        {
            _fixture = new Fixture();

            _fixture.Customize<UserRequest>(
                c => c
                    .With(u => u.EmailAddress, $"{_fixture.Create<string>()}@{_fixture.Create<string>()}.com")
                );
            RequestObject = _fixture.Create<UserRequest>();
        }

        private string ExtractEmailFieldFromTable(Table table, string fieldName, bool autoGenerate = false)
        {
            if (table.Rows.All(r => r["Field"] != fieldName))
            {
                return null;
            }

            var fieldValue = table.Rows.First(r => r["Field"] == fieldName)["Value"];

            if (autoGenerate)
            {
                fieldValue = (fieldValue == "{unique}")
                    ? $"{_fixture.Create<string>()}@{_fixture.Create<string>()}.com"
                    : fieldValue;
            }
            return fieldValue;
        }

        private string ExtractFieldFromTable(Table table, string fieldName, bool autoGenerate = false)
        {
            if (table.Rows.All(r => r["Field"] != fieldName))
            {
                return null;
            }

            var fieldValue = table.Rows.First(r => r["Field"] == fieldName)["Value"];

            if (autoGenerate)
            {
                fieldValue = (fieldValue == "{unique}")
                    ? $"{_fixture.Create<string>()}"
                    : fieldValue;
            }
            return fieldValue;
        }

        private Guid? ExtractGuidFieldFromTable(Table table, string fieldName, bool autoGenerate = false)
        {
            if (table.Rows.All(r => r["Field"] != fieldName))
            {
                return null;
            }

            var fieldValue = table.Rows.First(r => r["Field"] == fieldName)["Value"];

            if (autoGenerate)
            {
                if (fieldValue == "{unique}")
                {
                    return Guid.NewGuid();
                }
            }

            Guid guid;
            if (Guid.TryParse(fieldValue, out guid))
            {
                return guid;
            }

            return null;
        }

        private bool? ExtractBoolFieldFromTable(Table table, string fieldName)
        {
            if (table.Rows.All(r => r["Field"] != fieldName))
            {
                return null;
            }

            var fieldValue = table.Rows.First(r => r["Field"] == fieldName)["Value"];

            bool yesNo;
            if (bool.TryParse(fieldValue, out yesNo))
            {
                return yesNo;
            }

            return null;
        }

        private int? ExtractIntFieldFromTable(Table table, string fieldName, bool autoGenerate = false)
        {
            if (table.Rows.All(r => r["Field"] != fieldName))
            {
                return null;
            }

            var fieldValue = table.Rows.First(r => r["Field"] == fieldName)["Value"];

            if (autoGenerate)
            {
                fieldValue = (fieldValue == "{unique}")
                    ? $"{_fixture.Create<int>()}"
                    : fieldValue;
            }

            int id;
            if (int.TryParse(fieldValue, out id))
            {
                return id;
            }

            return null;
        }

        private DateTime? ExtractDateTimeFieldFromTable(Table table, string fieldName, bool autoGenerate = false)
        {
            if (table.Rows.All(r => r["Field"] != fieldName))
            {
                return null;
            }

            var fieldValue = table.Rows.First(r => r["Field"] == fieldName)["Value"];

            DateTime dateTime;

            if (autoGenerate)
            {
                if (fieldValue == "{today}")
                {
                    return DateTime.Today;
                }
                if (fieldValue == "{now}")
                {
                    return DateTime.UtcNow;
                }
            }
            if (!DateTime.TryParse(fieldValue, out dateTime))
            {
                return null;
            }
            return dateTime;
        }


        #region Given Steps
        [Given(@"a User Request of:")]
        public void GivenAUserRequestOf(Table table)
        {
            RequestObject.Merge(table.CreateInstance<UserRequest>());
        }

        [Given(@"a Publication of:")]
        public void GivenAPublicationOf(Table table)
        {
            RequestObject.Publication.Merge(table.CreateInstance<Publication>());
        }

        [Given(@"a UserDetails of:")]
        public void GivenAUserDetailsOf(Table table)
        {
            RequestObject.UserDetail.Merge(table.CreateInstance<UserDetail>());
        }

        [Given(@"no EmailAddress passed")]
        public void GivenNoEmailAddressPassed()
        {
            RequestObject.EmailAddress = null;
        }

        [Given(@"no ClientId is passed")]
        public void GivenNoClientIdIsPassed()
        {
            RequestObject.ClientId = null;
        }

        [Given(@"no ProductId is passed")]
        public void GivenNoProductIdIsPassed()
        {
            RequestObject.ProductId = null;
        }

        [Given(@"no FormId is passed")]
        public void GivenNoFormIdIsPassed()
        {
            RequestObject.FormId = null;
        }

        #endregion Given Steps
    }
}
