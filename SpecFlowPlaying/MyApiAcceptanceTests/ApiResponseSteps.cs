using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using MyApiAcceptanceTests.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;

namespace MyApiAcceptanceTests
{
    public abstract class ApiResponseSteps
    {
        protected HttpClient HttpClient;
        protected Uri RequestUri;
        protected HttpResponseMessage HttpResponseMessage;

        protected abstract Object RequestObject { get; }

        #region Given Steps
        [Given(@"I have a successful request")]
        public void GivenIHaveASuccessfulRequest()
        {
            RequestUri = new Uri("http://httpstat.us/200");
        }

        [Given(@"It also creates an item")]
        public void GivenItAlsoCreatesAnItem()
        {
            RequestUri = new Uri("http://httpstat.us/201");
        }

        [Given(@"It is also performed offline")]
        public void GivenItIsAlsoPerformedOffline()
        {
            RequestUri = new Uri("http://httpstat.us/202");
        }

        [Given(@"It also returns no content")]
        public void GivenItAlsoReturnsNoContent()
        {
            RequestUri = new Uri("http://httpstat.us/204");
        }

        [Given(@"I have a request that should not be sent again")]
        public void GivenIHaveARequestThatShouldNotBeSentAgain()
        {
            RequestUri = new Uri("http://httpstat.us/400");
        }

        [Given(@"It is also badly formed")]
        public void GivenItIsAlsoBadlyFormed()
        {
            RequestUri = new Uri("http://httpstat.us/400");
        }

        [Given(@"It also has a conflict")]
        public void GivenItAlsoHasAConflict()
        {
            RequestUri = new Uri("http://httpstat.us/409");
        }

        [Given(@"I have a request that fails but should be sent again")]
        public void GivenIHaveARequestThatFailsButShouldBeSentAgain()
        {
            RequestUri = new Uri("http://httpstat.us/500");
        }

        #endregion Given Steps

        #region When Steps

        [When(@"I call the Api")]
        public void WhenICallTheApi()
        {
            HttpClient = new HttpClient();

            var requestBody = JsonConvert.SerializeObject(RequestObject);

            HttpResponseMessage = HttpClient.PostAsync(
                RequestUri,
                new StringContent(requestBody),
                new JsonMediaTypeFormatter()).Result;
        }

        #endregion When Steps

        #region Then Steps
        [Then(@"the response has a status code of (\d{3})")]
        public void ThenTheResponseHasAStatusCodeOf(int statusCode)
        {
            var expected = (HttpStatusCode)statusCode;
            var actual = HttpResponseMessage.StatusCode;

            if (expected == actual)
            {
                Debug.Print($"Received expected response of {(int)expected} ({expected}).");
            }
            else
                Assert.Fail($"Received unexpected response of {(int)actual} ({actual}).");
        }

        [Then(@"it also has a response error message of (.+)")]
        public void ThenItAlsoHasAResponseErrorMessageOf(string expectedMessage)
        {
            var actual = JsonConvert.DeserializeObject<ErrorMessage>(
                HttpResponseMessage.Content.ReadAsStringAsync()
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
