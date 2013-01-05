using System;
using System.Configuration;
using Core.Domain;
using Core.DomainServices;
using Core.Mongo;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using NUnit.Framework;
using Rhino.Mocks;

namespace Core.IntegrationTests
{
    public class TestDomainObject : AggregateRoot
    {
        public TestDomainObject(Guid aggregateId)
        {
            ApplyChange(new TestDomainEvent
                {
                    AggregateId = aggregateId
                });
        }

        private void Apply(TestDomainEvent @event)
        {
            //Do Nout
        }
    }

    public class TestDomainEvent : DomainEvent
    {
        public Guid AggregateId { get; set; }
    }

    [TestFixture]
    public class TestMongoEventPersistence
    {
        private string _connectionString;
        private string _database;
        private IDomainRepository _domainRepository;

        [SetUp]
        public void Setup()
        {
            _connectionString = ConfigurationManager.AppSettings["MongoEventStoreConnectionString"];
            _database = ConfigurationManager.AppSettings["MongoEventStoreDatabaseName"];
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
            var aggregateRoot = new TestDomainObject(aggId);
            _domainRepository.Save(aggregateRoot);

            var client = new MongoClient(_connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase(_database);
            var collection = database.GetCollection<DomainEventCollection>(MongoEventPersistence.DOMAINEVENT_COLLECTION);
            var query = Query.EQ("AggregateId", aggId);
            var entity = collection.FindOne(query);
            Assert.That((object) entity.DomainEvents.Count, Is.EqualTo(1));
            Assert.That((object) entity.DomainEvents[0], Is.TypeOf<TestDomainEvent>());
        }

        [Test]
        public void ShouldGetEventsForAggregate()
        {
            var aggId = Guid.NewGuid();
            var aggregateRoot = new TestDomainObject(aggId);
            _domainRepository.Save(aggregateRoot);

            var retrievedAggregateRoot = _domainRepository.Get<TestDomainObject>(aggId);

            Assert.That((object) retrievedAggregateRoot.Version, Is.EqualTo(1));
        }
    }
}