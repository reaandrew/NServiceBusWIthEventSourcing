using Contact.Domain.DomainEvents;
using Core.Domain;
using Infrastructure.NServiceBus;
using NServiceBus;

namespace Contact.Infrastructure.NServiceBus.DomainEventMappers
{
    public class UserCreatedMapper : IEventMapper
    {
        public IEvent Map(DomainEvent @event)
        {
            var userCreatedEvent = (UserCreated) @event;
            return new Messages.Events.UserCreated
                {
                    UserID = userCreatedEvent.ID,
                    Name = userCreatedEvent.Name,
                    Email = userCreatedEvent.Email
                };
        }

        public bool CanMap(DomainEvent @event)
        {
            return @event.GetType() == typeof (UserCreated);
        }
    }
}