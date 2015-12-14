Feature: MyApiResponse

	In order to act on the outcome of an API request
	As an Client who sends a request
	I want to be told the outcome of the request

@mytag
Scenario: Request made without ClientId
	Given I have a successful request
	And it also is missing a ClientId
	When I call the Api
	Then the response has a status code of 400
	And it also has a response error message of Required ClientId not given

Scenario: Request made with ClientId that does not exist
	Given I have a successful request
	And it also has a ClientId that does not exist
	When I call the Api
	Then the response has a status code of 400
	And it also has a response error message of Invalid ClientId passed

Scenario: Request made without ProductId
	Given I have a successful request
	And it also is missing a ProductId
	When I call the Api
	Then the response has a status code of 400
	And it also has a response error message of Required ProductId not given

Scenario: Request made with ProductId that does not exist
	Given I have a successful request
	And it also has a ProductId that does not exist
	When I call the Api
	Then the response has a status code of 400
	And it also has a response error message of Invalid ProductId passed

Scenario: Request made without FormId
	Given I have a successful request
	And it also is missing a FormId
	When I call the Api
	Then the response has a status code of 400
	And it also has a response error message of Required FormId not given

Scenario: Request made with FormId that does not exist
	Given I have a successful request
	And it also has a FormId that does not exist
	When I call the Api
	Then the response has a status code of 400
	And it also has a response error message of Invalid FormId passed

Scenario: Request which is badly formed
	Given I have a request that should not be sent again
	And It is also badly formed
	When I call the Api
	Then the response has a status code of 400

Scenario: Requests which already exists
	Given I have a request that should not be sent again
	And It also has a conflict
	When I call the Api
	Then the response has a status code of 409

Scenario: Requests where an internal error occurs
	Given I have a request that fails but should be sent again
	When I call the Api
	Then the response has a status code of 500

Scenario: Request where the User Email is a string
	Given I have a successful request
	And it also has an Email Address of Not Valid Email
	When I call the Api
	Then the response has a status code of 400
	And it also has a response error message of Invalid Email Address passed

Scenario: Request where the User Email has a domain with no suffix
	Given I have a successful request
	And it also has an Email Address of another@failure
	When I call the Api
	Then the response has a status code of 400
	And it also has a response error message of Invalid Email Address passed

Scenario: Request which is Successful
	Given I have a successful request
	When I call the Api
	Then the response has a status code of 200

Scenario: Request which Creates an Item
	Given I have a successful request
	And It also creates an item
	When I call the Api
	Then the response has a status code of 201

Scenario: Request which is Performed Offline
	Given I have a successful request
	And It is also performed offline
	When I call the Api
	Then the response has a status code of 202

Scenario: Request which Returns No Content when it is successful
	Given I have a successful request
	And It also returns no content
	When I call the Api
	Then the response has a status code of 204