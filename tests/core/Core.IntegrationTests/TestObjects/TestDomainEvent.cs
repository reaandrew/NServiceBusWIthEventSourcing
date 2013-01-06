using System;
using Core.Domain;

namespace Core.IntegrationTests.TestObjects
{
    public class TestDomainEvent : DomainEvent
    {
        public Guid AggregateId { get; set; }
    }
}