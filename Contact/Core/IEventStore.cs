using System;
using System.Collections.Generic;
using Contact.Domain;

namespace Contact.Core
{
    public interface IEventStore
    {
        void SaveEvents(Guid id, List<DomainEvent> outstandingEvents);
        T Get<T>(Guid id) where T : AggregateRoot;
    }
}