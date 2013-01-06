using System;
using System.Configuration;
using System.Data.SqlClient;
using Core.DomainServices;
using Core.IntegrationTests.TestObjects;
using Core.Sql;
using NUnit.Framework;
using Rhino.Mocks;

namespace Core.IntegrationTests
{
    [TestFixture]
    public class TestSqlEventPersistence
    {
        private string _connectionString;
        private IDomainRepository _domainRepository;

        [SetUp]
        public void Setup()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["TestDatabase"].ConnectionString;
            var mockPublisher = MockRepository.GenerateMock<IEventPublisher>();
            var eventPersistence = SqlEventPersistence.Create(_connectionString, new[] {typeof (TestDomainEvent)});
            _domainRepository = new DomainRepository(new EventStore(eventPersistence, mockPublisher));
        }

        [TearDown]
        public void TearDown()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "truncate table Events";
                    command.ExecuteNonQuery();
                }
            }
        }

        [Test]
        public void ShouldPersistEventsToDatabase()
        {
            var aggId = Guid.NewGuid();
            var aggregateRoot = new TestDomainObject(aggId);
            _domainRepository.Save(aggregateRoot);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "Select COUNT(*) from Events";
                    var count = (int) command.ExecuteScalar();
                    Assert.That(count, Is.EqualTo(1));
                }
            }
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