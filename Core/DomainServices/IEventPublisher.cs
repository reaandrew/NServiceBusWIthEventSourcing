using Core.Domain;

namespace Core.DomainServices
{
    public interface IEventPublisher
    {
        void Publish<T>(T Event) where T : DomainEvent;
    }
}