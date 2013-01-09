namespace Core.InProc
{
    public class InProcEventPersistenceFactory : IEventPersistenceFactory
    {
        public IEventPersistence CreateEventPersistence()
        {
            return new InProcEventPersistence();
        }
    }
}