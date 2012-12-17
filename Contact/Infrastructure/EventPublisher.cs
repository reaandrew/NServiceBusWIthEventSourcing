using Contact.Core;
using NServiceBus;

namespace Contact.Infrastructure
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IBus _bus;

        public EventPublisher(IBus bus)
        {
            _bus = bus;
        }

        public void Publish<T>(T @event)
        {
            _bus.Publish(@event);
        }
    }
}
