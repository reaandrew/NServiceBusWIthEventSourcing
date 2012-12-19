using Contact.Domain;

namespace Contact.Core
{
    public interface IPublishEvent<in T>
        where T : DomainEvent
    {
        void Publish(T @event);
    }
}