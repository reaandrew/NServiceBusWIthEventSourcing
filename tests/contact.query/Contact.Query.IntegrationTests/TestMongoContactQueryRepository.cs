using System;
using System.Configuration;
using Contact.Query.Contracts.Model;
using Contact.Query.Mongo;
using MongoDB.Driver;
using NUnit.Framework;

namespace Contact.Query.IntegrationTests
{
    [TestFixture]
    public class TestMongoContactQueryRepository
    {
        private const string Name = "Name";
        private const string Email = "test@test.com";
        private string _connectionString;
        private string _databaseName;
        private readonly Guid _id = Guid.NewGuid();
        private MongoContactQueryRepository _repository;

        [SetUp]
        public void Setup()
        {
            _connectionString = ConfigurationManager.AppSettings["MongoEventStoreConnectionString"];
            _databaseName = ConfigurationManager.AppSettings["MongoEventStoreDatabaseName"];
            _repository = new MongoContactQueryRepository(_connectionString, _databaseName);
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
            const bool approved = false;
            var accommodationLead = new AccommodationLead
                {
                    AccommodationLeadId = _id,
                    Name = Name,
                    Email = Email,
                    Approved = approved
                };

            _repository.Save(accommodationLead);

            var entity = GetEntity<AccommodationLead>(_id, "accommodationLeads");

            Assert.That(entity, Is.Not.Null);
        }

        [Test]
        public void ShouldSaveAccommodationSupplier()
        {
            var accommodationSupplier = new AccommodationSupplier
                {
                    AccommodationSupplierId = _id,
                    Name = Name,
                    Email = Email
                };
            _repository.Save(accommodationSupplier);

            var entity = GetEntity<AccommodationSupplier>(_id, "accommodationSuppliers");

            Assert.That(entity, Is.Not.Null);
        }

        [Test]
        public void ShouldSaveUser()
        {
            var user = new User
                {
                    UserId = _id,
                    Name = Name,
                    Email = Email
                };
            _repository.Save(user);
            var entity = GetEntity<User>(_id, "users");
            Assert.That(entity, Is.Not.Null);
        }

        [Test]
        public void ShouldSaveAuthentication()
        {
            var authentication = new Authentication
                {
                    AuthenticationId = _id,
                    Email = Email,
                    HashedPassword = "Hash"
                };
            _repository.Save(authentication);
            var entity = GetEntity<Authentication>(_id, "authentications");
        }

        [Test]
        public void ShouldGetAccommodationLeadById()
        {
            var accommodationLead1 = new AccommodationLead
                {
                    AccommodationLeadId = _id,
                    Name = Name
                };
            var accommodationLead2 = new AccommodationLead
                {
                    AccommodationLeadId = Guid.NewGuid(),
                    Name = "Wrong"
                };
            _repository.Save(accommodationLead1);
            _repository.Save(accommodationLead2);

            var entity = _repository.GetAccommodationLeadById(_id);

            Assert.That(entity.Name, Is.EqualTo(Name));
        }

        [Test]
        public void ShouldListAccommodationLeads()
        {
            var accommodationLead1 = new AccommodationLead
                {
                    AccommodationLeadId = _id,
                    Name = Name
                };
            var accommodationLead2 = new AccommodationLead
                {
                    AccommodationLeadId = Guid.NewGuid(),
                    Name = "Wrong"
                };
            _repository.Save(accommodationLead1);
            _repository.Save(accommodationLead2);

            var entities = _repository.ListAccommodationLeads();
            Assert.That(entities.Count, Is.EqualTo(2));
        }

        private T GetEntity<T>(Guid id, string collectionName)
        {
            var collection = GetCollection<T>(collectionName);
            var query = MongoDB.Driver.Builders.Query.EQ("_id", _id);
            var entity = collection.FindOne(query);
            return entity.Object;
        }

        private MongoCollection<QueryObjectWrapper<T>> GetCollection<T>(string collectionName)
        {
            var client = new MongoClient(_connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase(_databaseName);
            var collection = database.GetCollection<QueryObjectWrapper<T>>(collectionName);
            return collection;
        }
    }
}