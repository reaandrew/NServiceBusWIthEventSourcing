using Core.Domain;
using NServiceBus;

namespace Infrastructure.NServiceBus
{
    public interface IEventMappings
    {
        IEvent GetMappedObjectFor(DomainEvent domainEvent);

        void AddMapper(IEventMapper mapper);
    }
}