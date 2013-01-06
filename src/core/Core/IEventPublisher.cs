using Core.Domain;

namespace Core
{
    public interface IEventPublisher
    {
        void Publish<T>(T Event) where T : DomainEvent;
    }
}