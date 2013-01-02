NOTE:

Please take the domain modelling in this example with a "pinch of salt".

The immediate goal is to look into different ways an EDA approach can help.

One fork in the road is looking at the application of Event Sourcing and how to efficiently use this whilst migrating away from legacy systems.
 - To do this I have used Simple CQRS by Greg Young as a guide to the AggregateRoot and Services Design
 - Made some slight amendments around the Replaying of events
 - Took the AsDynamic extension method as a whole from Simple CQRS
  - Using this as opposed to eplicility wiring up the handlers to events in the derived classes

This project has abstacted the persistence implementations for the Event Store and the Read Layer creation.
 - Looking at what technologies fit best for different situations

Currently using MSSQL For:
	-	Event Store (ADO.NET)
	-	Read Layer (EF 4)

Moving Forward Looking at:
	-	Redis, Mongo, Couchbase etc... for both the Event Store and Read Layers

Key Value Stores for the Event Store seem a really good fit.
