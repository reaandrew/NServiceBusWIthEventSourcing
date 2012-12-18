using Contact.Core;
using Contact.Domain;
using Contact.Messages.Events;

namespace Contact.Infrastructure.NServiceBus.DomainEventMappers
{
    public class AccommodationSupplierCreatedMapper : IMapDomainEvent<Domain.AccommodationSupplierCreated, AccSupplierCreated>
    {
        public AccSupplierCreated Map(AccommodationSupplierCreated @event)
        {
            return new AccSupplierCreated
                {
                    Name = @event.Name,
                    Email = @event.Email
                };
        }
    }
}