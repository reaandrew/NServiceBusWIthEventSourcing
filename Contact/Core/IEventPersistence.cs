using System;
using Contact.Domain;

namespace Contact.Core
{
    public interface IEventPersistence
    {
        void Save(Guid aggregateId, DomainEvent domainEvent);
    }
}