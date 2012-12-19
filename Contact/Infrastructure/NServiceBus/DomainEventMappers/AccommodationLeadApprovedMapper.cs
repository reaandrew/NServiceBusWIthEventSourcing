using Contact.Core;
using Contact.Domain;
using Core.Domain;
using NServiceBus;
using AccommodationLeadApproved = Contact.Messages.Events.AccommodationLeadApproved;

namespace Contact.Infrastructure.NServiceBus.DomainEventMappers
{
    public class AccommodationLeadApprovedMapper : IEventMapper
    {
        public IEvent Map(DomainEvent @event)
        {
            var accLeadCreatedEvent = (Domain.AccommodationLeadApproved) @event;
            return new AccommodationLeadApproved
            {
                AccLeadId = accLeadCreatedEvent.ID
            };
        }

        public bool CanMap(DomainEvent @event)
        {
            return @event.GetType() == typeof (Contact.Domain.AccommodationLeadApproved);
        }
    }
}