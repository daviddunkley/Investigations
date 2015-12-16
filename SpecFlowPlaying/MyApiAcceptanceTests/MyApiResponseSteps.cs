using MyApiAcceptanceTests.Models;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace MyApiAcceptanceTests
{
    [Binding]
    public class MyApiResponseSteps : ApiResponseSteps<UserRequest>
    {
        private readonly Fixture _fixture;

        public MyApiResponseSteps()
        {
            _fixture = new Fixture();
        }

        #region Given Steps

        [Given(@"a default UserRequest")]
        public void GivenADefaultUserRequest()
        {
            _fixture.Customize<UserRequest>(
                c => c
                    .With(u => u.EmailAddress, $"{_fixture.Create<string>()}@{_fixture.Create<string>()}.com")
                );
            RequestObject = _fixture.Create<UserRequest>();
        }

        [Given(@"a UserRequest updated by:")]
        public void GivenAUserRequestUpdatedBy(Table table)
        {
            RequestObject.Merge(table.CreateInstance<UserRequest>());
        }

        [Given(@"a Publication updated by:")]
        public void GivenAPublicationUpdatedBy(Table table)
        {
            RequestObject.Publication.Merge(table.CreateInstance<Publication>());
        }

        [Given(@"a UserDetail updated by:")]
        public void GivenAUserDetailUpdatedBy(Table table)
        {
            RequestObject.UserDetail.Merge(table.CreateInstance<UserDetail>());
        }

        [Given(@"no EmailAddress passed")]
        public void GivenNoEmailAddressPassed()
        {
            RequestObject.EmailAddress = null;
        }

        [Given(@"no ClientId is passed")]
        public void GivenNoClientIdIsPassed()
        {
            RequestObject.ClientId = null;
        }

        [Given(@"no ProductId is passed")]
        public void GivenNoProductIdIsPassed()
        {
            RequestObject.ProductId = null;
        }

        [Given(@"no FormId is passed")]
        public void GivenNoFormIdIsPassed()
        {
            RequestObject.FormId = null;
        }

        #endregion Given Steps
    }
}
