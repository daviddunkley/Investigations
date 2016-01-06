Feature: Users Import Api Method
	As a Titan Api Client
	I want to be able to create/update a User
	And receive a VerifyUrl
	So that I can give that VerifyUrl to a User and they can Verify their Subscription/Trial
	Because I don't want the responsibility of how the User receives the VerifyUrl

Scenario: Invalid ClientId
	Given an empty UserImport request
	And I set the ClientId to be "INVALID"
	When I make the Api User Import request
	Then that request needs to return a 500 - Internal Servce Error Response

Scenario: Invalid ProductId for valid ClientId
	Given a default UserImport request
	And I set the ClientId to be "hfi"
	When I make the Api User Import request
	Then that request needs to return a 400 - BadRequest Response
	And the response needs to have an error message of "Invalid ProductId passed for ClientId hfi"

Scenario: New User with Full Details
	Given a default UserImport request
	And I set the ClientId to be "hfi"
	And I set the ProductId to be "fce849c5-ce89-4c89-aa38-0bc37db3709a"
	When I make the Api User Import request
	Then that request needs to return a 201 - Created Response
	And the response needs to have a VerifyUrl Path of "/User/Verify"

Scenario: New User with No Details
	Given a default UserImport request
	And the UserDetails of:
         | Field             | Value          |
	When I make the Api User Import request
	Then that request needs to return a 400 - Bad Request Response
	And the response needs to have an error message of "Invalid User Details passed"

Scenario: New User with no FirstName
	Given a default UserImport request
	And the UserDetails of:
         | Field             | Value          |
         | title             | Mr             |
         | lastName          | User           |
         | jobTitle          | Test Job Title |
         | company           | Test Company   |
         | phoneNumber       | 02012345678    |
         | mobilePhoneNumber | 07012345678    |
         | address1          | Test Address 1 |
         | address2          | Test Address 2 |
         | address3          | Test Address 3 |
         | address4          | Test Address 4 |
         | address5          | Test Address 5 |
         | city              | Test City      |
         | postCode          | TT3 5TT        |
         | country           | United Kingdom |
	When I make the Api User Import request
	Then that request needs to return a 400 - BadRequest Response

Scenario: Existing User with a Subscription for the same Product
	Given a default UserImport request
	And I set the ClientId to be "hfi"
	And I set the ProductId to be "fce849c5-ce89-4c89-aa38-0bc37db3709a"
	And I have made the Api User Import request once
	When I make the Api User Import request again
	Then that request needs to return a 409 - Conflict Response

Scenario: Existing User with a Subscription for a different Product
	Given a default UserImport request
	And I set the ClientId to be "hfi"
	And I set the ProductId to be "fce849c5-ce89-4c89-aa38-0bc37db3709a"
	And I have made the Api User Import request once
	And I set the ProductId to be "7563d12d-dd21-4597-910f-2368e89f976b"
	When I make the Api User Import request again
	Then that request needs to return a 200 - OK Response
	And the response needs to have a VerifyUrl Path of "/User/Verify"
