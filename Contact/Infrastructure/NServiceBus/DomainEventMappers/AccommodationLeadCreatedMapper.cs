using Contact.Core;
using Contact.Messages.Events;

namespace Contact.Infrastructure.NServiceBus.DomainEventMappers
{
    public class AccommodationLeadCreatedMapper : IMapDomainEvent<Domain.AccommodationLeadCreated,Messages.Events.AccommodationLeadCreated>
    {
        public AccommodationLeadCreated Map(Domain.AccommodationLeadCreated @event)
        {
            return new AccommodationLeadCreated
                {
                    AccommodationLeadID = @event.ID,
                    Email = @event.Email,
                    Name = @event.Name
                };
        }
    }
}