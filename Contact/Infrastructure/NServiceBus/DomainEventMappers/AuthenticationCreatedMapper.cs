using System;
using Contact.Domain;
using Core.Domain;
using Infrastructure.NServiceBus;
using NServiceBus;

namespace Contact.Infrastructure.NServiceBus.DomainEventMappers
{
    public class AuthenticationCreatedMapper : IEventMapper
    {
        public IEvent Map(DomainEvent @event)
        {
            var authenticationCreatedEvent = (AuthenticationCreated) @event;
            return new Messages.Events.AuthenticationCreated
                {
                    AuthenticationID = authenticationCreatedEvent.ID,
                    Email = authenticationCreatedEvent.Email,
                    HashedPassword = authenticationCreatedEvent.HashedPassword
                };
        }

        public bool CanMap(DomainEvent @event)
        {
            return @event.GetType() == typeof (AuthenticationCreated);
        }
    }
}
