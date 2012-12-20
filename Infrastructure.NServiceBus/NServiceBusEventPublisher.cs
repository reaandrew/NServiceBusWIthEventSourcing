using Core;
using Core.Domain;
using NServiceBus;

namespace Infrastructure.NServiceBus
{
    /// <summary>
    ///     When using domain events, it isn't recommended to publish them directly on the bus - instead, a domain event handler would translate them to service-level events
    ///     http://stackoverflow.com/questions/2937248/nservicebus-without-spamming-my-solution-with-imessage-nservicebus-references
    /// </summary>
    public class NServiceBusEventPublisher : IEventPublisher
    {
        private readonly IBus _bus;
        private readonly IEventMappings _genericMappers;

        public NServiceBusEventPublisher(IBus bus, IEventMappings genericMappers)
        {
            _bus = bus;
            _genericMappers = genericMappers;
        }

        public void Publish<T>(T @event) where T : DomainEvent
        {
            var eventToPublish = _genericMappers.GetMappedObjectFor(@event);
            _bus.Publish(eventToPublish);
        }
    }
}