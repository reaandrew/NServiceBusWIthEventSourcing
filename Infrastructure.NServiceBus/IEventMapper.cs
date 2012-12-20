using Core.Domain;
using NServiceBus;

namespace Infrastructure.NServiceBus
{
    public interface IEventMapper
    {
        IEvent Map(DomainEvent @event);
        bool CanMap(DomainEvent @event);
    }
}