namespace Contact.Core
{
    public interface IMapDomainEvent<TDomainEventType, out TDestinationType>
    {
        TDestinationType Map(TDomainEventType @event);
    }
}