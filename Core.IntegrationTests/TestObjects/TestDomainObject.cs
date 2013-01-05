using System;
using Core.Domain;

namespace Core.IntegrationTests.TestObjects
{
    public class TestDomainObject : AggregateRoot
    {
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
        }
    }
}