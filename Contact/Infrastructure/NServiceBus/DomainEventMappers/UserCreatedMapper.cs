using Contact.Core;
using Contact.Messages.Events;

namespace Contact.Infrastructure.NServiceBus.DomainEventMappers
{
    public class UserCreatedMapper : IMapDomainEvent<Domain.UserCreated,Messages.Events.UserCreated>
    {
        public UserCreated Map(Domain.UserCreated @event)
        {
            return new UserCreated
                {
                    Name = @event.Name,
                    Email = @event.Email
                };
        }
    }
}