using Contact.Core;

namespace Contact.UnitTests.TestClasses
{
    public class EmptyDomainEventMapper : IMapDomainEvent<EmptyDomainEvent, EmptyNServiceBusEvent>
    {
        public EmptyNServiceBusEvent Map(EmptyDomainEvent @event)
        {
            return new EmptyNServiceBusEvent();
        }
    }
}