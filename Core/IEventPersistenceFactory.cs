namespace Core
{
    public interface IEventPersistenceFactory
    {
        IEventPersistence CreateEventPersistence();
    }
}