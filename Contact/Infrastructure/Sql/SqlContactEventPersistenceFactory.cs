using System;
using System.Configuration;
using System.Xml.Serialization;
using Contact.Domain.DomainEvents;
using Core;
using Core.Domain;
using Core.Sql;

namespace Contact.Infrastructure.Sql
{
    public class SqlContactEventPersistenceFactory : IEventPersistenceFactory
    {
        public IEventPersistence CreateEventPersistence()
        {
            var domainTypes = new[]
                {
                    typeof (DomainEvent),
                    typeof (AccommodationLeadCreated),
                    typeof (AccommodationLeadApproved),
                    typeof (UserCreated),
                    typeof (AuthenticationCreated),
                    typeof (AccommodationSupplierCreated)
                };
            var connectionString = ConfigurationManager.ConnectionStrings["TestDatabase"].ConnectionString;
            return SqlEventPersistence.Create(connectionString, domainTypes);
        }
    }
}
