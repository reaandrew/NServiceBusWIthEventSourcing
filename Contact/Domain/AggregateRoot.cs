using System;
using System.Collections.Generic;
using Contact.Infrastructure.Github;

namespace Contact.Domain
{
    public abstract class AggregateRoot
    {
        protected readonly List<DomainEvent> _outstandingEvents;
        private int _version;

        protected AggregateRoot()
        {
            _outstandingEvents = new List<DomainEvent>();
        }

        public Guid ID { get; protected set; }

        public List<DomainEvent> OutstandingEvents
        {
            get { return new List<DomainEvent>(_outstandingEvents); }
        }

        protected void ApplyChange<T>(T @event) where T : DomainEvent
        {
            @event.Version = ++_version;
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