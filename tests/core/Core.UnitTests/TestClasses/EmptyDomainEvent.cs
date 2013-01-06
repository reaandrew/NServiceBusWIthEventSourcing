using System;
using Core.Domain;

namespace Core.UnitTests.TestClasses
{
    public class EmptyDomainEvent : DomainEvent
    {
        public Guid ID { get; private set; }

        public EmptyDomainEvent(Guid id)
        {
            ID = id;
        }
    }
}