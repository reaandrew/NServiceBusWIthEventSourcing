using System;
using System.Collections.Generic;
using Core.Domain;

namespace Core
{
    public interface IEventStore
    {
        void SaveEvents(Guid id, List<DomainEvent> outstandingEvents);

        IList<DomainEvent> GetEventsForAggregate<T>(Guid id)
            where T : AggregateRoot;
    }
}