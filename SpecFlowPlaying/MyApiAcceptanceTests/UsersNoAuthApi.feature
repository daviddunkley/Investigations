Feature: UsersNoAuthApi

	As a Titan Api Client
	I want to be able to create/update a User
	And receive a VerifyUrl
	So that I can give that VerifyUrl to a User and they can Verify their Subscription/Trial
	Because I don't want the responsibility of how the User receives the VerifyUrl

Scenario: Invalid ClientId passed to /api/usersnoauth/import
	Given a default UserImport request
	And an Api Domain of "http://local-identitymanagement.ci03.global.root"
	When I will make the Api Request "POST /api/UsersNoAuth/Import"
	Then the method needs to return a 500 - Internal Servce Error Response

Scenario: Invalid ProductId passed for ClientId to /api/usersnoauth/import
	Given a default UserImport request
	And an Api Domain of "http://local-identitymanagement.ci03.global.root"
	And I set the ClientId to be "sandbox-ncu"
	When I will make the Api Request "POST /api/UsersNoAuth/Import"
	Then the method needs to return a 400 - BadRequest Response
	And the response needs to have an error message of "Invalid ProductId passed for ClientId sandbox-ncu"

Scenario: Valid request for a new User passed to /api/usersnoauth/import
	Given a default UserImport request
	And an Api Domain of "http://local-identitymanagement.ci03.global.root"
	And I set the ClientId to be "sandbox-ncu"
	And I set the ProductId to be "ed75561a-b35d-4846-88e3-6287228bc9bc"
	When I will make the Api Request "POST /api/UsersNoAuth/Import"
	Then the method needs to return a 201 - Created Response
	And the response needs to have a VerifyUrl Path of "http://local-identitymanagement.ci03.global.root/User/Verify"

Scenario: Valid request for an existing User with a Subscription for the same Product passed to /api/usersnoauth/import
	Given a default UserImport request
	And an Api Domain of "http://local-identitymanagement.ci03.global.root"
	And I set the ClientId to be "sandbox-ncu"
	And I set the ProductId to be "ed75561a-b35d-4846-88e3-6287228bc9bc"
	And I make the Api Request "POST /api/UsersNoAuth/Import"
	When I will make the Api Request "POST /api/UsersNoAuth/Import"
	Then the method needs to return a 409 - Conflict Response

Scenario: Valid request for an existing User with a Subscription for a different Product passed to /api/usersnoauth/import
	Given a default UserImport request
	And an Api Domain of "http://local-identitymanagement.ci03.global.root"
	And I set the ClientId to be "sandbox-ncu"
	And I set the ProductId to be "ed75561a-b35d-4846-88e3-6287228bc9bc"
	And I make the Api Request "POST /api/UsersNoAuth/Import"
	And I set the ProductId to be "ed75561a-b35d-4846-88e3-6287228bc9bc"
	When I will make the Api Request "POST /api/UsersNoAuth/Import"
	Then the method needs to return a 200 - OK Response
	And the response needs to have a VerifyUrl Path of "http://local-identitymanagement.ci03.global.root/User/Verify"
