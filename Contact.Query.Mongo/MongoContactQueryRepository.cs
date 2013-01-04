using System;
using System.Collections.Generic;
using Contact.Query.Contracts;
using Contact.Query.Contracts.Model;
using MongoDB.Driver;

namespace Contact.Query.Mongo
{
    public class MongoContactQueryRepository : IContactQueryRepository
    {
        private readonly string _connectionString;
        private readonly string _databaseName;

        public MongoContactQueryRepository(string connectionString, string databaseName)
        {
            _connectionString = connectionString;
            _databaseName = databaseName;
        }

        public void Save(AccommodationLead accommodationLead)
        {
            var client = new MongoClient(_connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase(_databaseName);
            var collection = database.GetCollection<QueryObjectWrapper<AccommodationLead>>("accommodationLeads");
            var wrappedObject = new QueryObjectWrapper<AccommodationLead>
                {
                    Object = accommodationLead,
                    _id = accommodationLead.AccommodationLeadId
                };
            collection.Save(wrappedObject);
        }

        public void Save(AccommodationSupplier accommodationSupplier)
        {
            throw new NotImplementedException();
        }

        public void Save(Authentication authentication)
        {
            throw new NotImplementedException();
        }

        public void Save(User user)
        {
            throw new NotImplementedException();
        }

        public List<AccommodationLead> ListAccommodationLeads()
        {
            throw new NotImplementedException();
        }

        public AccommodationLead GetAccommodationLeadById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}