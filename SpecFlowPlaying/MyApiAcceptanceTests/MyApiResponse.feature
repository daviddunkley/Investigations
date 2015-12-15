Feature: MyApiResponse

	In order to Upsert a User via an API request
	As an Client who sends a request
	I want to be told the outcome of the request

@mytag
Scenario: Request made with ClientId missing
	Given a standard request
	And a User with a name of Mr John Smith
	And a User with a company of Smith Ltd
	And it is missing a ClientId
	When I call the Api
	Then the response has a status code of 400
	And it has a response error message of Required ClientId not given

Scenario: Request made with ClientId that does not exist
	Given a standard request
	And a User with a name of Mr John Smith
	And a User with a company of Smith Ltd
	And it has a ClientId that does not exist
	When I call the Api
	Then the response has a status code of 400
	And it has a response error message of Invalid ClientId passed

Scenario: Request made with ProductId missing
	Given a standard request
	And a User with a name of Mr John Smith
	And a User with a company of Smith Ltd
	And it is missing a ProductId
	When I call the Api
	Then the response has a status code of 400
	And it has a response error message of Required ProductId not given

Scenario: Request made with ProductId that does not exist
	Given a standard request
	And a User with a name of Mr John Smith
	And a User with a company of Smith Ltd
	And it has a ProductId that does not exist
	When I call the Api
	Then the response has a status code of 400
	And it has a response error message of Invalid ProductId passed

Scenario: Request made with FormId missing
	Given a standard request
	And a User with a name of Mr John Smith
	And a User with a company of Smith Ltd
	And it is missing a FormId
	When I call the Api
	Then the response has a status code of 400
	And it has a response error message of Required FormId not given

Scenario: Request made with FormId that does not exist
	Given a standard request
	And a User with a name of Mr John Smith
	And a User with a company of Smith Ltd
	And it has a FormId that does not exist
	When I call the Api
	Then the response has a status code of 400
	And it has a response error message of Invalid FormId passed

Scenario: Request where the User Email is a string
	Given a standard request
	And a User with a name of Mr John Smith
	And a User with a company of Smith Ltd
	And it has an Email Address of Not Valid Email
	When I call the Api
	Then the response has a status code of 400
	And it has a response error message of Invalid Email Address passed

Scenario: Request where the User Email has a domain with no suffix
	Given a standard request
	And a User with a name of Mr John Smith
	And a User with a company of Smith Ltd
	And it has an Email Address of another@failure
	When I call the Api
	Then the response has a status code of 400
	And it has a response error message of Invalid Email Address passed

Scenario: Request which is Successful
	Given a standard request
	And a User with a name of Mr John Smith
	And a User with a company of Smith Ltd
	When I call the Api
	Then the response has a status code of 200

Scenario: Request where the User Title is not valid
	Given a standard request
	And a User with a title of Professor
	And a User with a first name of John
	And a User with a last name of Smith
	And a User with a company of Smith Ltd
	When I call the Api
	Then the response has a status code of 400
	And it has a response error message of Invalid Title passed
