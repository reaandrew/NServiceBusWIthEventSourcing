using Contact.Domain;
using Core.Domain;
using NServiceBus;

namespace Contact.Infrastructure.NServiceBus
{
    public interface IEventMapper
    {
        IEvent Map(DomainEvent @event);
        bool CanMap(DomainEvent @event);
    }
}