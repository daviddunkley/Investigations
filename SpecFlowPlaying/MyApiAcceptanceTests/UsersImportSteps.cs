using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using MyApiAcceptanceTests.Models;
using MyApiAcceptanceTests.Models.Titan;
using Newtonsoft.Json;
using NUnit.Framework;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace MyApiAcceptanceTests
{
    [Binding]
    public class UsersImportSteps
    {
        private readonly Fixture _fixture;
        private HttpClient _httpClient;
        private HttpResponseMessage _httpResponseMessage;

        public UsersImportSteps()
        {
            _fixture = new Fixture();
        }

        [BeforeFeature]
        public static void BeforeFeatureSetup()
        {
            FeatureContext.Current.Add("ApiDomain", new Uri("http://local-identitymanagement.ci03.global.root"));
            FeatureContext.Current.Add("DefaultClientId", "sandbox-ncu");
            FeatureContext.Current.Add("DefaultFormId", "fft");
            FeatureContext.Current.Add("DefaultProductId", new Guid("ed75561a-b35d-4846-88e3-6287228bc9bc"));

            var defaultUserDetails = new Dictionary<string, string>
            {
                {"title", "Mr"},
                {"firstName", "Test"},
                {"lastName", "User"},
                {"jobTitle", "Test Job Title"},
                {"company", "Test Company"},
                {"phoneNumber", "02012345678"},
                {"address1", "Test Address 1"},
                {"address2", "Test Address 2"},
                {"address3", "Test Address 3"},
                {"city", "Test City"},
                {"postCode", "TT3 5TT"},
                {"country", "United Kingdom"}
            };

            FeatureContext.Current.Add("DefaultUserDetails", defaultUserDetails);
        }

        [Given(@"an empty UserImport request")]
        public void GivenAnEmptyUserImportRequest()
        {
            ScenarioContext.Current.Add("UserImportRequest", new UserImportRequest());
        }

        [Given(@"a default UserImport request")]
        public void GivenADefaultUserImportRequest()
        {
            _fixture.Customize<UserImportRequest>(
                c => c
                    .With(u => u.EmailAddress, $"{_fixture.Create<string>()}@{_fixture.Create<string>()}.com")
                    .With(u => u.ClientId, FeatureContext.Current.Get<string>("DefaultClientId"))
                    .With(u => u.ProductId, FeatureContext.Current.Get<Guid>("DefaultProductId"))
                    .With(u => u.FormId, FeatureContext.Current.Get<string>("DefaultFormId"))
                    .With(u => u.Details, FeatureContext.Current.Get<Dictionary<string,string>>("DefaultUserDetails"))
                );

            ScenarioContext.Current.Add("UserImportRequest", _fixture.Create<UserImportRequest>());
        }

        [Given(@"the UserDetails of:")]
        public void GivenTheUserDetailsOf(Table table)
        {
            var userDetails = table.Rows.ToDictionary(row => row[0], row => row[1]);

            var userImportRequest = ScenarioContext.Current.Get<UserImportRequest>("UserImportRequest");
            userImportRequest.Details = userDetails;
        }

        [Given(@"I set the ClientId to be ""(.*)""")]
        public void GivenISetTheClientIdToBe(string clientId)
        {
            var userImportRequest = ScenarioContext.Current.Get<UserImportRequest>("UserImportRequest");
            userImportRequest.ClientId = clientId;
        }

        [Given(@"I set the FormId to be ""(.*)""")]
        public void GivenISetTheFormIdToBe(string formId)
        {
            var userImportRequest = ScenarioContext.Current.Get<UserImportRequest>("UserImportRequest");
            userImportRequest.FormId = formId;
        }


        [Given(@"I set the ProductId to be ""(.*)""")]
        public void GivenISetTheProductIdToBe(Guid productId)
        {
            var userImportRequest = ScenarioContext.Current.Get<UserImportRequest>("UserImportRequest");
            userImportRequest.ProductId = productId;
        }

        [Given(@"I have made the Api User Import request(.*)")]
        public void GivenIHaveMadeTheApiUserImportRequest(string p0)
        {
            var userImportRequest = ScenarioContext.Current.Get<UserImportRequest>("UserImportRequest");

            var httpResponseMessage = SendApiRequest(
                new Uri(FeatureContext.Current.Get<Uri>("ApiDomain"), TitanApiEndpoints.UsersImport),
                userImportRequest.ClientId,
                userImportRequest);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                Assert.Fail("Unable to Import User");
            }
        }

        [When(@"I make the Api User Import request(.*)")]
        public void WhenIMakeTheApiUserImportRequest(string p0)
        {
            var userImportRequest = ScenarioContext.Current.Get<UserImportRequest>("UserImportRequest");

            _httpResponseMessage = SendApiRequest(
                new Uri(FeatureContext.Current.Get<Uri>("ApiDomain"), TitanApiEndpoints.UsersImport),
                userImportRequest.ClientId,
                userImportRequest);
        }

        [Then(@"that request needs to return a (\d{3}) - (.*) Response")]
        public void ThenThatRequestNeedsToReturnAResponse(int statusCode, string statusName)
        {
            var expected = (HttpStatusCode)statusCode;
            var actual = _httpResponseMessage.StatusCode;

            if (expected == actual)
            {
                Console.WriteLine($"\tReceived expected response of {(int)expected} ({expected}).");
            }
            else
                Assert.Fail($"Received unexpected response of {(int)actual} ({actual}).");
        }

        [Then(@"the response needs to have an error message of ""(.+)""")]
        public void ThenTheResponseNeedsToHaveAnErrorMessageOf(string expectedMessage)
        {
            var responseBody = _httpResponseMessage.Content.ReadAsStringAsync().Result;

            var actual = JsonConvert.DeserializeObject<ErrorMessage>(responseBody);

            if (actual.Message == expectedMessage)
            {
                Debug.Print($"Received expected error message of '{expectedMessage}'.");
            }
            else
                Assert.Fail($"Received unexpected response of '{actual.Message}'.");
        }

        [Then(@"the response needs to have a VerifyUrl of null")]
        public void ThenTheResponseNeesToHaveAVerifyUrl()
        {
            var actual = JsonConvert.DeserializeObject<UserImportResponse>(
                _httpResponseMessage.Content.ReadAsStringAsync()
                    .Result);

            if (actual.VerifyUrl == null)
            {
                Console.WriteLine($"Received expected null VerifyUrl.");
            }
            else
            {
                Assert.Fail($"Received unexpected VerifyUrl of '{actual.VerifyUrl}'.");
            }
        }

        [Then(@"the response needs to have a VerifyUrl Path of ""(.*)""")]
        public void ThenTheResponseNeedsToHaveAVerifyUrlPathOf(string expectedVerifyUrl)
        {
            var actual = JsonConvert.DeserializeObject<UserImportResponse>(
                _httpResponseMessage.Content.ReadAsStringAsync()
                    .Result);

            var expectedVerifyUri = new Uri(FeatureContext.Current.Get<Uri>("ApiDomain"), expectedVerifyUrl);

            if (actual.VerifyUrl.GetLeftPart(UriPartial.Path) == expectedVerifyUri.GetLeftPart(UriPartial.Path))
            {
                Console.WriteLine($"Received expected VerifyUrl Path.");
            }
            else
                Assert.Fail($"Received unexpected VerifyUrl '{actual.VerifyUrl}'.");
        }

        private HttpResponseMessage SendApiRequest(Uri requestUri, string clientId, object requestBodyObject)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _httpClient.DefaultRequestHeaders
                .Add("X-ClientId", clientId);

            var requestBody = JsonConvert.SerializeObject(
                requestBodyObject,
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore,
                });

            // Console.WriteLine($"\tSending Request: {httpMethod} {apiUri}\n\n{requestBody}\n\n");

            return _httpClient.PostAsync(
                requestUri,
                new StringContent(requestBody, Encoding.UTF8, "application/json")
                ).Result;
        }
    }
}
