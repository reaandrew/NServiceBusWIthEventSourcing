using System;
using Contact.Query.Contracts.Model;
using Contact.Query.Mongo;
using MongoDB.Driver;
using NUnit.Framework;

namespace Contact.Query.IntegrationTests
{
    [TestFixture]
    public class TestMongoContactQueryRepository
    {
        private string _connectionString;
        private string _databaseName;

        [SetUp]
        public void Setup()
        {
            _connectionString = "mongodb://10.6.111.6";
            _databaseName = "integrationtests";
        }

        [TearDown]
        public void TearDown()
        {
            var client = new MongoClient(_connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase(_databaseName);
            server.DropDatabase(_databaseName);
        }

        [Test]
        public void ShouldSaveAccommodationLead()
        {
            var id = Guid.NewGuid();
            const string name = "Something";
            const string email = "test@test.com";
            const bool approved = false;
            var accommodationLead = new AccommodationLead
                {
                    AccommodationLeadId = id,
                    Name = name,
                    Email = email,
                    Approved = approved
                };
            var repository = new MongoContactQueryRepository(_connectionString, _databaseName);
            repository.Save(accommodationLead);

            var client = new MongoClient(_connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase(_databaseName);
            var collection = database.GetCollection<QueryObjectWrapper<AccommodationLead>>("accommodationLeads");
            var query = MongoDB.Driver.Builders.Query.EQ("_id", id);
            var entity = collection.FindOne(query);
            Assert.That(entity.Object, Is.Not.Null);
        }
    }
}