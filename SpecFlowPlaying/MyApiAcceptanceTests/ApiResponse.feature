Feature: ApiResponse

	In order to act on the outcome of an API request
	As an Client who sends a request
	I want to be told the outcome of the request

Scenario: Request which is Successful
	Given an Api request
	When I will call the HttpStat Api "Ok" Uri
	And I call the Api
	Then the response has a status code of 200

Scenario: Request which Creates an Item
	Given an Api request
	When I will call the HttpStat Api "Created" Uri
	And I call the Api
	Then the response has a status code of 201

Scenario: Request which is Performed Offline
	Given an Api request
	When I will call the HttpStat Api "Accepted" Uri
	And I call the Api
    Then the response has a status code of 202

Scenario: Request which is success but Returns No Content
	Given an Api request
	When I will call the HttpStat Api "No Content" Uri
	And I call the Api
	Then the response has a status code of 204

Scenario: Request which is badly formed
	Given an Api request
	When I will call the HttpStat Api "Bad Request" Uri
	And I call the Api
	Then the response has a status code of 400

Scenario: Requests which has a conflict
	Given an Api request
	When I will call the HttpStat Api "Conflict" Uri
	And I call the Api
	Then the response has a status code of 409

Scenario: Requests where an internal error occurs
	Given an Api request
	When I will call the HttpStat Api "System Error" Uri
	And I call the Api
	Then the response has a status code of 500
