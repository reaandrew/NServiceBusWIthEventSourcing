using Core.Domain;
using Infrastructure.NServiceBus;
using NServiceBus;
using AccommodationSupplierCreated = Contact.Messages.Events.AccommodationSupplierCreated;

namespace Contact.Infrastructure.NServiceBus.DomainEventMappers
{
    public class AccommodationSupplierCreatedMapper : IEventMapper
    {
        public IEvent Map(DomainEvent @event)
        {
            var accSupplierCreatedEvent = (Domain.DomainEvents.AccommodationSupplierCreated) @event;
            return new AccommodationSupplierCreated
                {
                    AccommodationSupplierId = accSupplierCreatedEvent.ID,
                    Name = accSupplierCreatedEvent.Name,
                    Email = accSupplierCreatedEvent.Email
                };
        }

        public bool CanMap(DomainEvent @event)
        {
            return @event.GetType() == typeof (Domain.DomainEvents.AccommodationSupplierCreated);
        }
    }
}