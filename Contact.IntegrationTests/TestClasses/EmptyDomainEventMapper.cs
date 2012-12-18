using Contact.Core;

namespace Contact.IntegrationTests.TestClasses
{
    public class EmptyDomainEventMapper : IMapDomainEvent<EmptyDomainEvent, EmptyNServiceBusEvent>
    {
        public EmptyNServiceBusEvent Map(EmptyDomainEvent @event)
        {
            return new EmptyNServiceBusEvent();
        }
    }
}