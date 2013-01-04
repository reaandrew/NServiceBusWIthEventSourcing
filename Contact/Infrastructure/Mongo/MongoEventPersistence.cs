using System;
using System.Collections.Generic;
using Core;
using Core.Domain;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Contact.Infrastructure.Mongo
{
    public class MongoEventPersistence : IEventPersistence
    {
        private readonly string _connectionString;
        private readonly string _database;

        static MongoEventPersistence()
        {
            BsonClassMap.RegisterClassMap<Domain.DomainEvents.AccommodationLeadApproved>();
            BsonClassMap.RegisterClassMap<Domain.DomainEvents.AccommodationLeadCreated>();
            BsonClassMap.RegisterClassMap<Domain.DomainEvents.AccommodationSupplierCreated>();
            BsonClassMap.RegisterClassMap<Domain.DomainEvents.AuthenticationCreated>();
            BsonClassMap.RegisterClassMap<Domain.DomainEvents.UserCreated>();
        }

        public MongoEventPersistence(string connectionString, string database)
        {
            _connectionString = connectionString;
            _database = database;
        }

        public void Save(Guid aggregateId, DomainEvent domainEvent)
        {
            var collection = GetCollection();
            var query = Query.EQ("AggregateId", aggregateId);
            var entity = collection.FindOne(query);
            if (entity == null)
            {
                var domainEventCollection = new DomainEventCollection
                    {
                        AggregateId = aggregateId,
                        DomainEvents = new List<DomainEvent> {domainEvent}
                    };
                collection.Insert(domainEventCollection);
            }
            else
            {
                entity.DomainEvents.Add(domainEvent);
                collection.Save(entity);
            }
        }

        public IList<DomainEvent> GetEventsForAggregate<T>(Guid id) where T : AggregateRoot
        {
            var collection = GetCollection();
            var query = Query.EQ("AggregateId", id);
            var entity = collection.FindOne(query);
            if (entity != null)
                return entity.DomainEvents;
            return new List<DomainEvent>();
        }

        private MongoCollection<DomainEventCollection> GetCollection()
        {
            var client = new MongoClient(_connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase(_database);
            var collection = database.GetCollection<DomainEventCollection>("domainEvents");
            return collection;
        }
    }
}