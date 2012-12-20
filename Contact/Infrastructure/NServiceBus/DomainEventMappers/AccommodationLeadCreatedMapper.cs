using Contact.Core;
using Contact.Domain;
using Core.Domain;
using Infrastructure.NServiceBus;
using NServiceBus;
using AccommodationLeadCreated = Contact.Messages.Events.AccommodationLeadCreated;

namespace Contact.Infrastructure.NServiceBus.DomainEventMappers
{
    public class AccommodationLeadCreatedMapper : IEventMapper
    {
        public IEvent Map(DomainEvent @event)
        {
            var accLeadCreatedEvent = (Domain.DomainEvents.AccommodationLeadCreated) @event;
            return new AccommodationLeadCreated
                {
                    AccommodationLeadID = accLeadCreatedEvent.ID,
                    Email = accLeadCreatedEvent.Email,
                    Name = accLeadCreatedEvent.Name
                };
        }

        public bool CanMap(DomainEvent @event)
        {
            return @event.GetType() == typeof (Domain.DomainEvents.AccommodationLeadCreated);
        }
    }
}