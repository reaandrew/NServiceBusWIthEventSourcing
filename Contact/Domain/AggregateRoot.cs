using System;
using System.Collections.Generic;
using System.Linq;
using Contact.Infrastructure.Github;

namespace Contact.Domain
{
    public abstract class AggregateRoot
    {
        private readonly List<DomainEvent> _outstandingEvents;
        public int Version { get; private set; }
        public Guid ID { get; protected set; }

        protected AggregateRoot()
        {
            _outstandingEvents = new List<DomainEvent>();
        }

        protected AggregateRoot(IEnumerable<DomainEvent> domainEvents)
            :this()
        {
            var events = domainEvents.OrderBy(x => x.Version);
            foreach (var @event in events)
            {
                ReplayChange(@event);
            }
        }

        public List<DomainEvent> OutstandingEvents
        {
            get { return new List<DomainEvent>(_outstandingEvents); }
        }

        protected void ReplayChange<T>(T @event) where T : DomainEvent
        {
            Version = @event.Version;
            this.AsDynamic().Apply(@event);
        }

        protected void ApplyChange<T>(T @event) where T : DomainEvent
        {
            @event.Version = ++Version;
            _outstandingEvents.Add(@event);

            //This is simply a way to avoid extra code at the cost of using
            //reflection, provided by external code from CQRS.
            //A reflection less way would be to map events to handlers which I am
            //not against but I though I would give this a try as I liked the look
            //of it.
            this.AsDynamic().Apply(@event);
        }

        public void MarkChangesAsCommitted()
        {
            _outstandingEvents.Clear();
        }
    }
}