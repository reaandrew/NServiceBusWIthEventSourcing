Intended test strategy
==
 - It would be nice to be able to test like this.  
 - This would come after the end to end test ofcourse and allow for mocking of infrastrcuture components
 - NServiceBus supports this testing with a fluent interface
 
Tests
==
When I send an ApproveAccLead Command
Then an AccLeadApproved Event is pubilshed

When an AccLeadApproved Event is published
Then a CreateAccSupplier Command is sent

When a CreateAccSupplier Command is sent
Then a CreateUser Command is sent

When a UserCreated Event is raised
And the UserCreated Event is correlated to the creation of an AccSupplier
Then an AccSupplierCreated Event is published

When an AccSupplierCreated Event is published
Then a SendAccSupplierRegistrationConfirmation Command is sent
