using Contact.Domain;

namespace Contact.Core
{
    public interface IPublishEvent
    {
        
    }

    public interface IPublishEvent<in T> : IPublishEvent
        where T : DomainEvent
    {
        void Publish(T @event);
    }
}