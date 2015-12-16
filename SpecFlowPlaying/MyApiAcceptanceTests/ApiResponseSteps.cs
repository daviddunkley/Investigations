using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using MyApiAcceptanceTests.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace MyApiAcceptanceTests
{
   // [Binding]
    public class ApiResponseSteps<T>
    {
        private HttpClient _httpClient;
        private Uri _requestUri;
        private HttpResponseMessage _httpResponseMessage;
        protected T RequestObject;

        [Given(@"an Api request")]
        public void GivenAnApiRequest()
        {
            RequestObject = default(T);
        }

        #region When Steps

        [When(@"I will call the HttpStat Api ""(.*)"" Uri")]
        public void WhenIWillCallTheHttpStatApiUri(string httpStatApiUri)
        {
            // Remove whitespace
            httpStatApiUri = Regex.Replace(httpStatApiUri, @"\s+", "");

            // Lookup the Field name using Reflection
            var apiUri = typeof(HttpStatApiUris).GetField(httpStatApiUri).GetValue(null);

            // Assign the Request Uri
            _requestUri = (Uri)apiUri;
        }

        [When(@"I will call the Mocky Api ""(.*)"" Uri")]
        public void WhenIWillCallTheMockyApiUri(string mockyApiUri)
        {
            // Remove whitespace
            mockyApiUri = Regex.Replace(mockyApiUri, @"\s+", "");

            // Lookup the Field name using Reflection
            var apiUri = typeof(MockyApiUris).GetField(mockyApiUri).GetValue(null);

            // Assign the Request Uri
            _requestUri = (Uri)apiUri;
        }

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
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore,
                });

            Console.WriteLine($"\tSending Request: POST {_requestUri}");
            Console.WriteLine($"\tSending Request Body:\n\n{requestBody}\n\n");

            _httpResponseMessage = _httpClient.PostAsync(
                _requestUri,
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

        [Then(@"the response has an error message of (.+)")]
        public void ThenTheResponseHasAnErrorMessageOf(string expectedMessage)
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

        #endregion Then Steps
    }
}
