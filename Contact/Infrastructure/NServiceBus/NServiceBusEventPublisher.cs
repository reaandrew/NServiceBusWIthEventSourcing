using Contact.Core;
using Contact.Domain;
using NServiceBus;

namespace Contact.Infrastructure.NServiceBus
{
    /// <summary>
    ///     When using domain events, it isn't recommended to publish them directly on the bus - instead, a domain event handler would translate them to service-level events
    ///     http://stackoverflow.com/questions/2937248/nservicebus-without-spamming-my-solution-with-imessage-nservicebus-references
    /// </summary>
    public class NServiceBusEventPublisher : IEventPublisher
    {
        private readonly IBus _bus;
        private readonly IDomainEventMappingCollection<IEvent> _domainEventGenericMappers;

        public NServiceBusEventPublisher(IBus bus, IDomainEventMappingCollection<IEvent> domainEventGenericMappers)
        {
            _bus = bus;
            _domainEventGenericMappers = domainEventGenericMappers;
        }

        public void Publish<T>(T @event) where T : DomainEvent
        {
            var eventToPublish = _domainEventGenericMappers.GetMappedEventFor(@event);
            _bus.Publish(eventToPublish);
        }
    }
}