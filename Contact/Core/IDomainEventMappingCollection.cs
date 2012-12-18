namespace Contact.Core
{
    public interface IDomainEventMappingCollection<TDestinationType>
    {
        TDestinationType GetMappedEventFor<TDomainEventType>(TDomainEventType domainEvent);
    }
}