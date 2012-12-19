using Contact.Domain;
using NServiceBus;
using UserCreated = Contact.Messages.Events.UserCreated;

namespace Contact.Infrastructure.NServiceBus.DomainEventMappers
{
    public class UserCreatedMapper : IEventMapper
    {
        public IEvent Map(DomainEvent @event)
        {
            var userCreatedEvent = (Domain.UserCreated) @event;
            return new UserCreated
            {
                UserID = userCreatedEvent.ID,
                Name = userCreatedEvent.Name,
                Email = userCreatedEvent.Email
            };
        }

        public bool CanMap(DomainEvent @event)
        {
            return @event.GetType() == typeof (Domain.UserCreated);
        }
    }
}