using Contact.Core;
using Contact.Messages.Events;

namespace Contact.Infrastructure.NServiceBus.DomainEventMappers
{
    public class AccommodationLeadApprovedMapper 
        : IMapDomainEvent<Domain.AccommodationLeadApproved,AccommodationLeadApproved>
    {
        public AccommodationLeadApproved Map(Domain.AccommodationLeadApproved @event)
        {
            return new AccommodationLeadApproved
                {
                    AccLeadId = @event.ID
                };
        }
    }
}