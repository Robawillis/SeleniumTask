Feature: OrderBookFromAmazon
	As customer at Amazon 
	I want to be able to add a book called "A Game of Thrones (A Song of Ice and Fire, Book1)" into the basket as a paperback
	So that I can Review the book in the basket   

@Amazon_HomePage
Scenario: 01 Navigate to Amazon
	Given I enter "http://www.amazon.co.uk" in Chrome's address bar
	When I instruct it to go to the address
	Then I expect Amazon's home page to display

@Book_Search
	Scenario: 02 Search for Book
	Given I enter "Game of Thrones" in Amazon's Search bar
	And I select the "books" option in the Amazon Search bar category
	When I click on the search button
	Then I expect to see Amazon's search results page with relevant search results

@Book_Details
	Scenario: 03 Book details
	Given I Have the amazon results page
	And the book on test is present in the results list
	When I click on the item in the results list
	Then I expect to See the books details


@Adding book to basket
	Scenario: 04 Adding book to basket
	Given the books details are correct
	And I have the paperback option selected
	When I click on the "Add to basket" option
	Then I expect to see the "Added to basket" notification page

@Editing Basket
	Scenario: 05 Basket verification
	Given I am on the basket notification page
	When I click on the "Edit basket" button
	Then I expect to see the Shopping basket page with the item present, quantity and subtotal correct

