using System;
using Contact.DomainServices;
using Contact.Infrastructure.InProc;
using Contact.Infrastructure.NServiceBus;
using NServiceBus;

namespace Contact.IntegrationTests.ProcessorsTests
{
    public static class TestProcessorFactory
    {
        public static T Create<T>(IBus bus)
        {
            var mapperFactory = new NServiceBusDomainEventMappingFactory();
            var mapperCollection = mapperFactory.CreateMappingCollection();
            var eventPersistence = new InProcEventPersistence();
            var eventPublisher = new NServiceBusEventPublisher(bus, mapperCollection);
            var eventStore = new EventStore(eventPersistence, eventPublisher);
            var instance = Activator.CreateInstance(typeof (T), eventStore);
            return (T)instance;
        } 
    }
}