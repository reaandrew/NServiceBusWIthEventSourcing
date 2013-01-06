using Contact.Domain.DomainEvents;
using Core.Domain;
using Infrastructure.NServiceBus;
using NServiceBus;

namespace Contact.Infrastructure.NServiceBus.DomainEventMappers
{
    public class AccommodationLeadCreatedMapper : IEventMapper
    {
        public IEvent Map(DomainEvent @event)
        {
            var accLeadCreatedEvent = (AccommodationLeadCreated) @event;
            return new Messages.Events.AccommodationLeadCreated
                {
                    AccommodationLeadID = accLeadCreatedEvent.ID,
                    Email = accLeadCreatedEvent.Email,
                    Name = accLeadCreatedEvent.Name
                };
        }

        public bool CanMap(DomainEvent @event)
        {
            return @event.GetType() == typeof (AccommodationLeadCreated);
        }
    }
}