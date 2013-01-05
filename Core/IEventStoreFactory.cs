namespace Core
{
    public interface IEventStoreFactory
    {
        IEventPersistence CreateEventPersistence();
    }
}