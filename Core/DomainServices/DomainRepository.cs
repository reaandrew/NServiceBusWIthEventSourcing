using System;
using Core.Domain;

namespace Core.DomainServices
{
    public class DomainRepository : IDomainRepository
    {
        private readonly IEventStore _eventStore;

        public DomainRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public void Save<T>(T aggregateRoot) 
            where T : AggregateRoot
        {
            _eventStore.SaveEvents(aggregateRoot.ID, aggregateRoot.OutstandingEvents);
            aggregateRoot.MarkChangesAsCommitted();
        }

        public T Get<T>(Guid id) where T : AggregateRoot
        {
            var events = _eventStore.GetEventsForAggregate<T>(id);
            var aggregateRoot = (T)Activator.CreateInstance(typeof(T), new object[] { events });
            return aggregateRoot;
        }
    }
}