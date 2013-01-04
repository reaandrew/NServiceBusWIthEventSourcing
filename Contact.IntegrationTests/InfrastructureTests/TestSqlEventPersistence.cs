using System;
using System.Configuration;
using System.Data.SqlClient;
using Contact.Domain;
using Contact.Infrastructure.Sql;
using Core;
using Core.DomainServices;
using NUnit.Framework;
using Rhino.Mocks;

namespace Contact.IntegrationTests.InfrastructureTests
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
            var eventPersistence = new SqlEventPersistence(_connectionString);
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
            const string name = "Joe";
            const string email = "test@test.com";
            var aggregateRoot = new AccommodationLead(aggId, name, email);
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
            const string name = "Joe";
            const string email = "test@test.com";
            var aggregateRoot = new AccommodationLead(aggId, name, email);
            _domainRepository.Save(aggregateRoot);

            var retrievedAggregateRoot = _domainRepository.Get<AccommodationLead>(aggId);

            Assert.That(retrievedAggregateRoot.Version, Is.EqualTo(1));
        }
    }
}