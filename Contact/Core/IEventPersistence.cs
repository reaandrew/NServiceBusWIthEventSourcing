using System;
using System.Collections.Generic;
using Contact.Domain;

namespace Contact.Core
{
    public interface IEventPersistence
    {
        void Save(Guid aggregateId, DomainEvent domainEvent);
        IList<DomainEvent> GetEventsForAggregate<T>(Guid id) where T : AggregateRoot;
    }
}