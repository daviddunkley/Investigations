using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using MyApiAcceptanceTests.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;

namespace MyApiAcceptanceTests
{
    public abstract class ApiResponseSteps
    {
        protected readonly Uri _httpStatUri = new Uri("http://httpstat.us/");
        protected readonly Uri _http200Uri = new Uri("http://httpstat.us/200");

        private HttpClient _httpClient;
        protected Uri RequestUri;
        private HttpResponseMessage _httpResponseMessage;

        protected abstract Object RequestObject { get; set; }

        #region Given Steps
        [Given(@"a successful request")]
        public void GivenASuccessfulRequest()
        {
            RequestUri = new Uri(_httpStatUri, "200");
        }

        [Given(@"it creates an item")]
        public void GivenItCreatesAnItem()
        {
            RequestUri = new Uri(_httpStatUri, "201");
        }

        [Given(@"it is performed offline")]
        public void GivenItIsPerformedOffline()
        {
            RequestUri = new Uri(_httpStatUri, "202");
        }

        [Given(@"it returns no content")]
        public void GivenItReturnsNoContent()
        {
            RequestUri = new Uri(_httpStatUri, "204");
        }

        [Given(@"a request that should not be sent again")]
        public void GivenARequestThatShouldNotBeSentAgain()
        {
            RequestUri = new Uri(_httpStatUri, "400");
        }

        [Given(@"it is badly formed")]
        public void GivenItIsBadlyFormed()
        {
            RequestUri = new Uri(_httpStatUri, "400");
        }

        [Given(@"it has a conflict")]
        public void GivenItHasAConflict()
        {
            RequestUri = new Uri(_httpStatUri, "409");
        }

        [Given(@"a request that fails but should be sent again")]
        public void GivenARequestThatFailsButShouldBeSentAgain()
        {
            RequestUri = new Uri(_httpStatUri, "500");
        }

        #endregion Given Steps

        #region When Steps

        [When(@"I call the Api")]
        public void WhenICallTheApi()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var requestBody = JsonConvert.SerializeObject(
                RequestObject,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            _httpResponseMessage = _httpClient.PostAsync(
                RequestUri,
                new StringContent(requestBody, Encoding.UTF8, "application/json")
                ).Result;
        }

        #endregion When Steps

        #region Then Steps
        [Then(@"the response has a status code of (\d{3})")]
        public void ThenTheResponseHasAStatusCodeOf(int statusCode)
        {
            var expected = (HttpStatusCode)statusCode;
            var actual = _httpResponseMessage.StatusCode;

            if (expected == actual)
            {
                Debug.Print($"Received expected response of {(int)expected} ({expected}).");
            }
            else
                Assert.Fail($"Received unexpected response of {(int)actual} ({actual}).");
        }

        [Then(@"it has a response error message of (.+)")]
        public void ThenItHasAResponseErrorMessageOf(string expectedMessage)
        {
            var actual = JsonConvert.DeserializeObject<ErrorMessage>(
                _httpResponseMessage.Content.ReadAsStringAsync()
                    .Result);

            if (actual.Message == expectedMessage)
            {
                Debug.Print($"Received expected error message of '{expectedMessage}'.");
            }
            else
                Assert.Fail($"Received unexpected response of '{actual.Message}'.");
        }

        #endregion When Steps
    }
}
