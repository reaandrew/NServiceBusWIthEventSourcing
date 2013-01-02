using Core.Domain;
using Infrastructure.NServiceBus;
using NServiceBus;
using AccommodationLeadApproved = Contact.Messages.Events.AccommodationLeadApproved;

namespace Contact.Infrastructure.NServiceBus.DomainEventMappers
{
    public class AccommodationLeadApprovedMapper : IEventMapper
    {
        public IEvent Map(DomainEvent @event)
        {
            var accommodationLeadApproved = (Domain.DomainEvents.AccommodationLeadApproved) @event;
            return new AccommodationLeadApproved
            {
                AccLeadId = accommodationLeadApproved.ID,
                Name = accommodationLeadApproved.Name,
                Email = accommodationLeadApproved.Email
            };
        }

        public bool CanMap(DomainEvent @event)
        {
            return @event.GetType() == typeof (Domain.DomainEvents.AccommodationLeadApproved);
        }
    }
}