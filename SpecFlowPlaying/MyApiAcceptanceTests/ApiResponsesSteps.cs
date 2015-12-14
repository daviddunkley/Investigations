using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace MyApiAcceptanceTests
{
    [Binding]
    public class ApiResponsesSteps
    {
        private HttpClient _httpClient;
        private int _expectedResponseCode;
        private HttpResponseMessage _httpResponseMessage;

        [Given(@"I have a successful request")]
        public void GivenIHaveASuccessfulRequest()
        {
            _expectedResponseCode = 200;
        }

        [Given(@"a user with the following:")]
        public void GivenAUserWithTheFollowing(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"It also creates an item")]
        public void GivenItAlsoCreatesAnItem()
        {
            _expectedResponseCode = 201;
        }

        [Given(@"It is also performed offline")]
        public void GivenItIsAlsoPerformedOffline()
        {
            _expectedResponseCode = 202;
        }

        [Given(@"It also returns no content")]
        public void GivenItAlsoReturnsNoContent()
        {
            _expectedResponseCode = 204;
        }

        [Given(@"I have a request that should not be sent again")]
        public void GivenIHaveARequestThatShouldNotBeSentAgain()
        {
            _expectedResponseCode = 400;
        }

        [Given(@"It is also badly formed")]
        public void GivenItIsAlsoBadlyFormed()
        {
            _expectedResponseCode = 400;
        }

        [Given(@"It also has a conflict")]
        public void GivenItAlsoHasAConflict()
        {
            _expectedResponseCode = 409;
        }

        [Given(@"I have a request that fails but should be sent again")]
        public void GivenIHaveARequestThatFailsButShouldBeSentAgain()
        {
            _expectedResponseCode = 500;
        }

        [When(@"I say (.+)")]
        public void WhenISayWTF(string message)
        {
            Debug.WriteLine(message);
        }

        [When(@"I call the Api")]
        public void WhenICallTheApi()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("http://httpstat.us/"),
            };

            _httpResponseMessage = _httpClient.PostAsync($"{_expectedResponseCode}", null).Result;
        }

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
    }
}
