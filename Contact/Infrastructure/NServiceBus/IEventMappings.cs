using Contact.Domain;
using NServiceBus;

namespace Contact.Infrastructure.NServiceBus
{
    public interface IEventMappings
    {
        IEvent GetMappedObjectFor(DomainEvent domainEvent);

        void AddMapper(IEventMapper mapper);
    }
}