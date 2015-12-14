using System;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;

namespace MyApiAcceptanceTests
{
    [Binding]
    public class MyApiResponseSteps : ApiResponseSteps
    {
        protected override Object RequestObject { get; }

        public MyApiResponseSteps()
        {
            RequestObject = new Fixture().Create<UserRequest>();
        }

        #region Given Steps

        [Given(@"it also is missing a ClientId")]
        public void GivenItAlsoIsMissingAClientId()
        {
            ((UserRequest)RequestObject).ClientId = null;
            RequestUri = new Uri("http://www.mocky.io/v2/566ee5aa100000d629718e30");
        }

        [Given(@"it also is missing a ProductId")]
        public void GivenItAlsoIsMissingAProductId()
        {
            ((UserRequest)RequestObject).ProductId = null;
            RequestUri = new Uri("http://www.mocky.io/v2/566ee2551000002729718e2d");
        }

        [Given(@"it also is missing a FormId")]
        public void GivenItAlsoIsMissingAFormId()
        {
            ((UserRequest)RequestObject).FormId = null;
            RequestUri = new Uri("http://www.mocky.io/v2/566ee2a71000002729718e2e");
        }

        [Given(@"it also has a ClientId that does not exist")]
        public void GivenItAlsoHasAClientIdThatDoesNotExist()
        {
            RequestUri = new Uri("http://www.mocky.io/v2/566ed77e100000b526718e1f");
        }

        [Given(@"it also has a ProductId that does not exist")]
        public void GivenItAlsoHasAProductIdThatDoesNotExist()
        {
            RequestUri = new Uri("http://www.mocky.io/v2/566ee5da100000d629718e32");
        }

        [Given(@"it also has a FormId that does not exist")]
        public void GivenItAlsoHasAFormIdThatDoesNotExist()
        {
            RequestUri = new Uri("http://www.mocky.io/v2/566ed898100000f526718e21");
        }

        #endregion Given Steps
    }
}
