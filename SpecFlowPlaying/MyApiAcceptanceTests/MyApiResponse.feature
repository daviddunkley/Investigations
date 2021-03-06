﻿Feature: MyApiResponse

	In order to Upsert a User via an API request
	As an Client who sends a request
	I want to be told the outcome of the request

Scenario: Valid request for a new User with all fields provided
	Given a default UserRequest
	When I will call the HttpStat Api "Created" Uri
	And I call the Api
	Then the response has a status code of 201

Scenario: Valid request for a new User with minimum fields provided
	Given a default UserRequest
	When I will call the HttpStat Api "Created" Uri
	And I call the Api
	Then the response has a status code of 201

Scenario: Valid request for existing User with all fields provided
	Given a default UserRequest
	And a UserRequest updated by:
		| Field        | Value                                |
		| EmailAddress | john.smith@test.com                  |
		| ClientId     | mb                                   |
		| ProductId    | 37636234-aa8a-4818-b3d0-28840321a05c |
		| FormId       | mb_user_import                       |
	And a Publication updated by:
		| Field                | Value                                          |
		| PublicationId        | 502                                            |
		| StartDate            | {now}                                          |
		| ContentViewed        | http://local-titanclient-ncu.ci03.global.root/ |
		| AdditionalParameters | {"fid":"all"}                                  |
		| SendEmail            | true                                           |
	And a UserDetail updated by:
         | Field   | Value          |
         | Field   | Value          |
         | Title   | Mr             |
         | Country | United Kingdom |
	When I will call the HttpStat Api "Ok" Uri
	And I call the Api
	Then the response has a status code of 200

Scenario: Request made with EmailAddress missing
	Given a default UserRequest
	And no EmailAddress passed
	When I will call the Mocky Api "Required EmailAddress Not Given" Uri
	And I call the Api
	Then the response has a status code of 400
	And the response has an error message of Required EmailAddress not given

Scenario: Request made with EmailAddress without domain
	Given a default UserRequest
	And a UserRequest updated by:
		| Field        | Value             |
		| EmailAddress | NOT EMAIL ADDRESS |
	When I will call the Mocky Api "Invalid EmailAddress Passed" Uri
	And I call the Api
	Then the response has a status code of 400
	And the response has an error message of Invalid Email Address passed

Scenario: Request made with EmailAddress without suffix
	Given a default UserRequest
	And a UserRequest updated by:
		| Field        | Value     |
		| EmailAddress | no@suffix |
	When I will call the Mocky Api "Invalid EmailAddress Passed" Uri
	And I call the Api
	Then the response has a status code of 400
	And the response has an error message of Invalid Email Address passed

Scenario: Request made with ClientId missing
	Given a default UserRequest
	And no ClientId is passed
	When I will call the Mocky Api "Required Client Id Not Given" Uri
	And I call the Api
	Then the response has a status code of 400
	And the response has an error message of Required ClientId not given

Scenario: Request made with ClientId that does not exist
	Given a default UserRequest
	And a UserRequest updated by:
		| Field        | Value               |
		| ClientId     | NOT VALID CLIENT ID |
	When I will call the Mocky Api "Invalid ClientId Passed" Uri
	And I call the Api
	Then the response has a status code of 400
	And the response has an error message of Invalid ClientId passed

Scenario: Request made with ProductId missing
	Given a default UserRequest
	And no ProductId is passed
	When I will call the Mocky Api "Required Product Id Not Given" Uri
	And I call the Api
	Then the response has a status code of 400
	And the response has an error message of Required ProductId not given

Scenario: Request made with ProductId that does not exist
	Given a default UserRequest
	And a UserRequest updated by:
		| Field        | Value     |
		| ProductId    | 37636234-aa8a-4818-b3d0-28840321a05c |
	When I will call the Mocky Api "Invalid ProductId Passed" Uri
	And I call the Api
	Then the response has a status code of 400
	And the response has an error message of Invalid ProductId passed

Scenario: Request made with FormId missing
	Given a default UserRequest
	And no FormId is passed
	When I will call the Mocky Api "Required FormId Not Given" Uri
	And I call the Api
	Then the response has a status code of 400
	And the response has an error message of Required FormId not given

Scenario: Request made with FormId that does not exist
	Given a default UserRequest
	Given a UserRequest updated by:
		| Field        | Value                                |
		| FormId       | NOT VALID FORMID                     |
	When I will call the Mocky Api "Invalid FormId Passed" Uri
	And I call the Api
	Then the response has a status code of 400
	And the response has an error message of Invalid FormId passed

Scenario: Request where the User Title is not valid
	Given a default UserRequest
	Given a UserRequest updated by:
		| Field        | Value                                |
		| ClientId     | mb                                   |
		| ProductId    | 37636234-aa8a-4818-b3d0-28840321a05c |
		| FormId       | mb_user_import                       |
	And a Publication updated by:
		| Field                | Value                                          |
		| PublicationId        | 502                                            |
		| StartDate            | 0001-01-01T00:00:00                            |
		| ContentViewed        | http://local-titanclient-ncu.ci03.global.root/ |
		| AdditionalParameters | {"fid":"all"}                                  |
		| SendEmail            | true                                           |
	And a UserDetail updated by:
		| Field     | Value          |
		| Title     | Professor      |
		| Country   | United Kingdom |
	When I will call the Mocky Api "Invalid Title Passed" Uri
	And I call the Api
	Then the response has a status code of 400
	And the response has an error message of Invalid Title passed
