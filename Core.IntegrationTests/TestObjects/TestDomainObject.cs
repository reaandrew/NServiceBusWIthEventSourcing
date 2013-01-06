using System;
using System.Collections.Generic;
using Core.Domain;

namespace Core.IntegrationTests.TestObjects
{
    public class TestDomainObject : AggregateRoot
    {
        public TestDomainObject(IEnumerable<DomainEvent> domainEvents)
            : base(domainEvents)
        {
        }

        public TestDomainObject(Guid aggregateId)
        {
            ApplyChange(new TestDomainEvent
                {
                    AggregateId = aggregateId
                });
        }

        private void Apply(TestDomainEvent @event)
        {
            //Do Nout
            this.ID = @event.AggregateId;
        }
    }
}