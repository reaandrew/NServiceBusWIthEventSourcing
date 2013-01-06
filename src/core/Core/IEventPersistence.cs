using System;
using System.Collections.Generic;
using Core.Domain;

namespace Core
{
    public interface IEventPersistence
    {
        void Save(Guid aggregateId, DomainEvent domainEvent);
        IList<DomainEvent> GetEventsForAggregate<T>(Guid id) where T : AggregateRoot;
    }
}