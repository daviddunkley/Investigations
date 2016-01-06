using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
    public class UsersNoAuthApiSteps
    {
        private readonly Fixture _fixture;
        private Uri _apiDomain;
        private UserImportRequest _userImportRequest;
        private HttpClient _httpClient;
        private HttpResponseMessage _httpResponseMessage;

        public UsersNoAuthApiSteps()
        {
            _fixture = new Fixture();
        }

        [Given(@"a default UserImport request")]
        public void GivenADefaultUserImportRequest()
        {
            _fixture.Customize<UserImportRequest>(
                c => c
                    .With(u => u.EmailAddress, $"{_fixture.Create<string>()}@{_fixture.Create<string>()}.com")
                );
            _userImportRequest = _fixture.Create<UserImportRequest>();
        }

        [Given(@"an Api Domain of ""(.*)""")]
        public void GivenAnApiDomainOf(string apiDomain)
        {
            _apiDomain = new Uri(apiDomain);
        }

        [Given(@"I set the ClientId to be ""(.*)""")]
        public void GivenISetTheClientIdToBe(string clientId)
        {
            _userImportRequest.ClientId = clientId;
        }

        [Given(@"I set the ProductId to be ""(.*)""")]
        public void GivenISetTheProductIdToBe(Guid productId)
        {
            _userImportRequest.ProductId = productId;
        }

        [Given(@"I make the Api Request ""(.*) (.*)""")]
        public void GivenIMakeTheApiRequest(HttpMethod httpMethod, string apiEndpoint)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _httpClient.DefaultRequestHeaders
                .Add("X-ClientId", _userImportRequest.ClientId);


            var requestBody = JsonConvert.SerializeObject(
                _userImportRequest,
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore,
                });

            // Console.WriteLine($"\tSending Request: {httpMethod} {apiUri}\n\n{requestBody}\n\n");

            var requestUri = new Uri(_apiDomain, apiEndpoint);

            _httpResponseMessage = _httpClient.PostAsync(
                requestUri,
                new StringContent(requestBody, Encoding.UTF8, "application/json")
                ).Result;
        }


        [When(@"I will make the Api Request ""(.*) (.*)""")]
        public void WhenIWillMakeTheApiRequest(string httpMethod, string apiEndpoint)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _httpClient.DefaultRequestHeaders
                .Add("X-ClientId", _userImportRequest.ClientId);


            var requestBody = JsonConvert.SerializeObject(
                _userImportRequest,
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore,
                });

            var requestUri = new Uri(_apiDomain, apiEndpoint);

            // Console.WriteLine($"\tSending Request: {httpMethod} {apiUri}\n\n{requestBody}\n\n");

            _httpResponseMessage = _httpClient.PostAsync(
                requestUri,
                new StringContent(requestBody, Encoding.UTF8, "application/json")
                ).Result;
        }

        [Then(@"the method needs to return a (\d{3}) - (.*) Response")]
        public void ThenTheMethodNeedsToReturnAResponse(int statusCode, string statusName)
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
        public void ThenTheResponseNeesToHaveAVerifyUrlPathOf(string expectedVerifyUrl)
        {
            var actual = JsonConvert.DeserializeObject<UserImportResponse>(
                _httpResponseMessage.Content.ReadAsStringAsync()
                    .Result);

            var expectedVerifyUri = new Uri(expectedVerifyUrl);

            if (actual.VerifyUrl.GetLeftPart(UriPartial.Path) == expectedVerifyUri.GetLeftPart(UriPartial.Path))
            {
                Console.WriteLine($"Received expected VerifyUrl Path.");
            }
            else
                Assert.Fail($"Received unexpected VerifyUrl '{actual.VerifyUrl}'.");
        }
    }
}
