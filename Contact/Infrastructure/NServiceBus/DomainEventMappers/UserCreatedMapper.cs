using Contact.Domain;
using Core.Domain;
using Infrastructure.NServiceBus;
using NServiceBus;
using UserCreated = Contact.Messages.Events.UserCreated;

namespace Contact.Infrastructure.NServiceBus.DomainEventMappers
{
    public class UserCreatedMapper : IEventMapper
    {
        public IEvent Map(DomainEvent @event)
        {
            var userCreatedEvent = (Domain.DomainEvents.UserCreated) @event;
            return new UserCreated
            {
                UserID = userCreatedEvent.ID,
                Name = userCreatedEvent.Name,
                Email = userCreatedEvent.Email
            };
        }

        public bool CanMap(DomainEvent @event)
        {
            return @event.GetType() == typeof (Domain.DomainEvents.UserCreated);
        }
    }
}