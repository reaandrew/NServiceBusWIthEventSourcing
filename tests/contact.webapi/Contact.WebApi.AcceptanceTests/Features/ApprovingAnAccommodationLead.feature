Feature: ApprovingAnAccommodationLead
	In order to increase the company revenue
	As a Product Improvement user
	I want to be able to approve valid accommodation leads
	And increase the stock of hotels which can be advertised

Scenario: Approve a valid AccommodationLead
	Given an Accommodation Lead exists with the following information:
	| Name       | Email        |
	| Joe Bloggs | joe@test.com |
	When I approve the Accommodation Lead
	And I query the system for the Accommodation Lead
	Then the Approval status of the Accommodation Lead should be true