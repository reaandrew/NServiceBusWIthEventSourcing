using System.Configuration;
using Contact.Domain.DomainEvents;
using Core;
using Core.Mongo;
using MongoDB.Bson.Serialization;

namespace Contact.Infrastructure.Mongo
{
    /// <summary>
    /// This will be the ContactMongoEventStoreFactory
    /// </summary>
    public class MongoContactEventPersistenceFactory : IEventPersistenceFactory
    {
        static MongoContactEventPersistenceFactory()
        {
            BsonClassMap.RegisterClassMap<AccommodationLeadApproved>();
            BsonClassMap.RegisterClassMap<AccommodationLeadCreated>();
            BsonClassMap.RegisterClassMap<AccommodationSupplierCreated>();
            BsonClassMap.RegisterClassMap<AuthenticationCreated>();
            BsonClassMap.RegisterClassMap<UserCreated>();
        }

        public IEventPersistence CreateEventPersistence()
        {
            var mongoConnectionString = ConfigurationManager.AppSettings["MongoEventStoreConnectionString"];
            var mongoDatabase = ConfigurationManager.AppSettings["MongoEventStoreDatabaseName"];
            return new MongoEventPersistence(mongoConnectionString, mongoDatabase);
        }
    }
}