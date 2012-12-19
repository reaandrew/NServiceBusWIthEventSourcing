using Contact.Core;
using Contact.Domain;
using Contact.Messages.Events;
using Core.Domain;
using NServiceBus;

namespace Contact.Infrastructure.NServiceBus.DomainEventMappers
{
    public class AccommodationSupplierCreatedMapper : IEventMapper
    {
        public IEvent Map(DomainEvent @event)
        {
            var accSupplierCreatedEvent = (Domain.AccommodationSupplierCreated) @event;
            return new AccSupplierCreated
                {
                    Name = accSupplierCreatedEvent.Name,
                    Email = accSupplierCreatedEvent.Email
                };
        }

        public bool CanMap(DomainEvent @event)
        {
            return @event.GetType() == typeof (Domain.AccommodationSupplierCreated);
        }
    }
}