using Contact.Domain.DomainEvents;
using Core.Domain;
using Infrastructure.NServiceBus;
using NServiceBus;

namespace Contact.Infrastructure.NServiceBus.DomainEventMappers
{
    public class AccommodationSupplierCreatedMapper : IEventMapper
    {
        public IEvent Map(DomainEvent @event)
        {
            var accSupplierCreatedEvent = (AccommodationSupplierCreated) @event;
            return new Messages.Events.AccommodationSupplierCreated
                {
                    AccommodationSupplierId = accSupplierCreatedEvent.ID,
                    Name = accSupplierCreatedEvent.Name,
                    Email = accSupplierCreatedEvent.Email
                };
        }

        public bool CanMap(DomainEvent @event)
        {
            return @event.GetType() == typeof (AccommodationSupplierCreated);
        }
    }
}