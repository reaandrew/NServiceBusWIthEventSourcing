using System;
using System.Collections.Generic;
using Core.Domain;
using Core.DomainServices;

namespace Contact.DomainServices
{
    public class EventStore : IEventStore
    {
        private readonly IEventPersistence _eventPersistence;
        private readonly IEventPublisher _eventPublisher;

        public EventStore(IEventPersistence eventPersistence, IEventPublisher eventPublisher)
        {
            _eventPersistence = eventPersistence;
            _eventPublisher = eventPublisher;
        }

        public void SaveEvents(Guid id, List<DomainEvent> outstandingEvents)
        {
            foreach (DomainEvent @event in outstandingEvents)
            {
                _eventPersistence.Save(id, @event);
                _eventPublisher.Publish(@event);
            }
        }

        public IList<DomainEvent> GetEventsForAggregate<T>(Guid id)
            where T : AggregateRoot
        {
            return _eventPersistence.GetEventsForAggregate<T>(id);
        }
    }
}