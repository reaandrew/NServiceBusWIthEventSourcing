using System;
using System.Collections.Generic;
using System.Linq;
using Contact.Query.Contracts;
using Contact.Query.Contracts.Model;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Contact.Query.Mongo
{
    public class MongoContactQueryRepository : IContactQueryRepository
    {
        public const string ACCOMMODATIONLEAD_COLLECTION = "accommodationLeads";
        public const string ACCOMMODATIONSUPPLIER_COLLECTION = "accommodationSuppliers";
        public const string USER_COLLECTION = "users";
        public const string AUTHENTICATION_COLLECTION = "authentications";

        private readonly string _connectionString;
        private readonly string _databaseName;

        public MongoContactQueryRepository(string connectionString, string databaseName)
        {
            _connectionString = connectionString;
            _databaseName = databaseName;
        }

        public void Save(AccommodationLead accommodationLead)
        {
            var collection = GetCollection<AccommodationLead>(ACCOMMODATIONLEAD_COLLECTION);
            var wrappedObject = new QueryObjectWrapper<AccommodationLead>
                {
                    Object = accommodationLead,
                    _id = accommodationLead.AccommodationLeadId
                };
            collection.Save(wrappedObject);
        }

        public void Save(AccommodationSupplier accommodationSupplier)
        {
            var collection = GetCollection<AccommodationSupplier>(ACCOMMODATIONSUPPLIER_COLLECTION);
            var wrappedObject = new QueryObjectWrapper<AccommodationSupplier>
            {
                Object = accommodationSupplier,
                _id = accommodationSupplier.AccommodationSupplierId
            };
            collection.Save(wrappedObject);
        }

        public void Save(Authentication authentication)
        {
            var collection = GetCollection<Authentication>(AUTHENTICATION_COLLECTION);
            var wrappedObject = new QueryObjectWrapper<Authentication>
            {
                Object = authentication,
                _id = authentication.AuthenticationId
            };
            collection.Save(wrappedObject);
        }

        public void Save(User user)
        {
            var collection = GetCollection<User>(USER_COLLECTION);
            var wrappedObject = new QueryObjectWrapper<User>
            {
                Object = user,
                _id = user.UserId
            };
            collection.Save(wrappedObject);
        }

        public List<AccommodationLead> ListAccommodationLeads()
        {
            var collection = GetCollection<AccommodationLead>(ACCOMMODATIONLEAD_COLLECTION);
            var entities = collection.FindAll();
            return entities.Select(x => x.Object).ToList();
        }

        public AccommodationLead GetAccommodationLeadById(Guid id)
        {
            var collection = GetCollection<AccommodationLead>(ACCOMMODATIONLEAD_COLLECTION);
            var query = MongoDB.Driver.Builders.Query.EQ("_id", id);
            var entity = collection.FindOne(query);
            return entity != null ? entity.Object : null;
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