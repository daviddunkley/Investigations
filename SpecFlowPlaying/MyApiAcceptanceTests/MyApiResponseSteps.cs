using System;
using System.Diagnostics;
using System.Linq;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;

namespace MyApiAcceptanceTests
{
    [Binding]
    public class MyApiResponseSteps : ApiResponseSteps
    {
        private readonly Fixture _fixture;
        protected override Object RequestObject { get; set; }
        private string[] _titles { get; }

        public MyApiResponseSteps()
        {
            _fixture = new Fixture();

            _titles = new string[]
            {
                "Mr",
                "Mrs",
                "Ms",
                "Miss",
            };
        }
        #region Given Steps

        [Given(@"a standard request")]
        public void GivenAStandardRequest()
        {
            _fixture.Customize<UserRequest>(
                c => c
                    .With(u => u.EmailAddress, $"{_fixture.Create<string>()}@test.com")
                );

            UserRequest userRequest = _fixture.Create<UserRequest>();

            RequestObject = userRequest;

            RequestUri = new Uri(_httpStatUri, "200");
        }


        [Given(@"a User with a title of (.*)")]
        public void GivenAUserWithATitle(string title)
        {
            ((UserRequest)RequestObject).UserDetail.Title = title;

            if (!_titles.Contains(title))
            {
                RequestUri = MockyApiUris.InvalidTitlePassed;
            }
        }

        [Given(@"a User with a first name of (.*)")]
        public void GivenAUserWithAFirstName(string firstName)
        {
            ((UserRequest)RequestObject).UserDetail.FirstName = firstName;
        }

        [Given(@"a User with a last name of (.*)")]
        public void GivenAUserWithALastName(string lastName)
        {
            ((UserRequest) RequestObject).UserDetail.LastName = lastName;
        }

        [Given(@"a User with a company of (.*)")]
        public void GivenAUserWithACompanyName(string companyName)
        {
            ((UserRequest)RequestObject).UserDetail.Company = companyName;
        }

        [Given(@"a User with a name of (.*) (.*) (.*)")]
        public void GivenAUserWithAName(string title, string firstName, string lastName)
        {
            var userRequest = ((UserRequest) RequestObject);
            userRequest.UserDetail.Title = title;
            userRequest.UserDetail.FirstName = firstName;
            userRequest.UserDetail.LastName = lastName;

            RequestObject = userRequest;
        }

        [Given(@"it is missing a ClientId")]
        public void GivenItIsMissingAClientId()
        {
            ((UserRequest)RequestObject).ClientId = null;
            RequestUri = MockyApiUris.RequiredClientIdNotGiven;
        }

        [Given(@"it is missing a ProductId")]
        public void GivenItIsMissingAProductId()
        {
            ((UserRequest)RequestObject).ProductId = null;

            RequestUri = MockyApiUris.RequiredProductIdNotGiven;
        }

        [Given(@"it is missing a FormId")]
        public void GivenItIsMissingAFormId()
        {
            ((UserRequest)RequestObject).FormId = null;

            RequestUri = MockyApiUris.RequiredFormIdNotGiven;
        }

        [Given(@"it has a ClientId that does not exist")]
        public void GivenItHasAClientIdThatDoesNotExist()
        {
            RequestUri = MockyApiUris.InvalidClientIdPassed;
        }

        [Given(@"it has a ProductId that does not exist")]
        public void GivenItHasAProductIdThatDoesNotExist()
        {
            RequestUri = MockyApiUris.InvalidProductIdPassed;
        }

        [Given(@"it has a FormId that does not exist")]
        public void GivenItHasAFormIdThatDoesNotExist()
        {
            RequestUri = MockyApiUris.InvalidFormIdPassed;
        }

        [Given(@"it has an Email Address of (.+)")]
        public void GivenItHasAnEmailAddressOfNotValidEmail(string emailAddress)
        {
            ((UserRequest) RequestObject).EmailAddress = emailAddress;
            RequestUri = MockyApiUris.InvalidEmailAddressPassed;
        }

        #endregion Given Steps
    }
}
