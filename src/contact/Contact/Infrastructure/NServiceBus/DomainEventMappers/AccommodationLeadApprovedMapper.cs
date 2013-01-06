using Contact.Domain.DomainEvents;
using Core.Domain;
using Infrastructure.NServiceBus;
using NServiceBus;

namespace Contact.Infrastructure.NServiceBus.DomainEventMappers
{
    public class AccommodationLeadApprovedMapper : IEventMapper
    {
        public IEvent Map(DomainEvent @event)
        {
            var accommodationLeadApproved = (AccommodationLeadApproved) @event;
            return new Messages.Events.AccommodationLeadApproved
                {
                    AccLeadId = accommodationLeadApproved.ID,
                    Name = accommodationLeadApproved.Name,
                    Email = accommodationLeadApproved.Email
                };
        }

        public bool CanMap(DomainEvent @event)
        {
            return @event.GetType() == typeof (AccommodationLeadApproved);
        }
    }
}