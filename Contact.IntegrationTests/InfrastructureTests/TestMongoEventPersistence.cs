using System;
using Contact.Domain;
using Contact.Domain.DomainEvents;
using Contact.Infrastructure.Mongo;
using Core;
using Core.DomainServices;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using NUnit.Framework;
using Rhino.Mocks;

namespace Contact.IntegrationTests.InfrastructureTests
{
    [TestFixture]
    public class TestMongoEventPersistence
    {
        private string _connectionString;
        private string _database;
        private IDomainRepository _domainRepository;

        [SetUp]
        public void Setup()
        {
            _connectionString = "mongodb://10.6.111.6";
            _database = "integrationtests";
            var mockPublisher = MockRepository.GenerateMock<IEventPublisher>();
            var eventPersistence = new MongoEventPersistence(_connectionString, _database);
            _domainRepository = new DomainRepository(new EventStore(eventPersistence, mockPublisher));
        }

        [TearDown]
        public void TearDown()
        {
            var client = new MongoClient(_connectionString);
            var server = client.GetServer();
            server.DropDatabase(_database);
        }

        [Test]
        public void ShouldPersistEventsToDatabase()
        {
            var aggId = Guid.NewGuid();
            const string name = "Joe";
            const string email = "test@test.com";
            var aggregateRoot = new AccommodationLead(aggId, name, email);
            _domainRepository.Save(aggregateRoot);

            var client = new MongoClient(_connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase(_database);
            var collection = database.GetCollection<DomainEventCollection>("domainEvents");
            var query = Query.EQ("AggregateId", aggId);
            var entity = collection.FindOne(query);
            Assert.That(entity.DomainEvents.Count, Is.EqualTo(1));
            Assert.That(entity.DomainEvents[0], Is.TypeOf<AccommodationLeadCreated>());
        }

        [Test]
        public void ShouldGetEventsForAggregate()
        {
            var aggId = Guid.NewGuid();
            const string name = "Joe";
            const string email = "test@test.com";
            var aggregateRoot = new AccommodationLead(aggId, name, email);
            _domainRepository.Save(aggregateRoot);

            var retrievedAggregateRoot = _domainRepository.Get<AccommodationLead>(aggId);

            Assert.That(retrievedAggregateRoot.Version, Is.EqualTo(1));
        }
    }
}