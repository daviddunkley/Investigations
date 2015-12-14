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
            RequestUri = MockyApiUris.RequiredClientIdNotGiven;
        }

        [Given(@"it also is missing a ProductId")]
        public void GivenItAlsoIsMissingAProductId()
        {
            ((UserRequest)RequestObject).ProductId = null;
            RequestUri = MockyApiUris.RequiredProductIdNotGiven;
        }

        [Given(@"it also is missing a FormId")]
        public void GivenItAlsoIsMissingAFormId()
        {
            ((UserRequest)RequestObject).FormId = null;
            RequestUri = MockyApiUris.RequiredFormIdNotGiven;
        }

        [Given(@"it also has a ClientId that does not exist")]
        public void GivenItAlsoHasAClientIdThatDoesNotExist()
        {
            RequestUri = MockyApiUris.InvalidClientIdPassed;
        }

        [Given(@"it also has a ProductId that does not exist")]
        public void GivenItAlsoHasAProductIdThatDoesNotExist()
        {
            RequestUri = MockyApiUris.InvalidProductIdPassed;
        }

        [Given(@"it also has a FormId that does not exist")]
        public void GivenItAlsoHasAFormIdThatDoesNotExist()
        {
            RequestUri = MockyApiUris.InvalidFormIdPassed;
        }

        [Given(@"it also has an Email Address of (.+)")]
        public void GivenItAlsoHasAnEmailAddressOfNotValidEmail(string emailAddress)
        {
            ((UserRequest) RequestObject).EmailAddress = emailAddress;
            RequestUri = MockyApiUris.InvalidEmailAddressPassed;
        }

        [Given(@"also an Invalid User Title of (.+)")]
        public void GivenAlsoAnInvalidUserTitleOf(string title)
        {
            ((UserRequest)RequestObject).UserDetail.Title = title;
            RequestUri = MockyApiUris.InvalidTitlePassword;
        }

        #endregion Given Steps
    }
}
