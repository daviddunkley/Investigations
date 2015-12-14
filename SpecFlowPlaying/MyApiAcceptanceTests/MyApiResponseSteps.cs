using System;
using System.Linq;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;

namespace MyApiAcceptanceTests
{
    [Binding]
    public class MyApiResponseSteps : ApiResponseSteps
    {
        private readonly Fixture _fixture;
        protected override Object RequestObject { get; }

        public MyApiResponseSteps()
        {
            _fixture = new Fixture();
            _fixture.Customize<UserRequest>(
                c => c
                    .With(u => u.EmailAddress, $"{_fixture.Create<string>()}@test.com")
                );

            UserRequest userRequest = _fixture.Create<UserRequest>();
            userRequest.UserDetail.Title = "Mrs";

            RequestObject = userRequest;
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

        [Given(@"it also has an Email Address of (.+)")]
        public void GivenItAlsoHasAnEmailAddressOfNotValidEmail(string emailAddress)
        {
            ((UserRequest) RequestObject).EmailAddress = emailAddress;
            RequestUri = new Uri("http://www.mocky.io/v2/566eeb41100000132a718e3a");
        }

        [Given(@"also an Invalid User Title of (.+)")]
        public void GivenAlsoAnInvalidUserTitleOf(string title)
        {
            ((UserRequest)RequestObject).UserDetail.Title = title;
            RequestUri = new Uri("http://www.mocky.io/v2/566ef4031000009b2c718e3f");
        }

        #endregion Given Steps
    }
}
