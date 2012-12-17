using NServiceBus;

namespace Contact.Core
{
    public class EventPublisher : IPublishEvent
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
