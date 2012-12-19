using System;
using System.Collections.Generic;
using Contact.Domain;

namespace Contact.Core
{
    public interface IEventStore
    {
        void SaveEvents(Guid id, List<DomainEvent> outstandingEvents);

        IList<DomainEvent> GetEventsForAggregate<T>(Guid id)
            where T : AggregateRoot;
    }
}