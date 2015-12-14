Feature: ApiResponses

	In order to act on the outcome of an API request
	As an Client who sends a request
	I want to be told the outcome of the request

@mytag
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
