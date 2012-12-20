using Contact.Core;
using Contact.Infrastructure.InProc;
using Contact.Infrastructure.NServiceBus;
using Core;
using Core.DomainServices;
using NServiceBus;

namespace Contact.IntegrationTests.ProcessorsTests.SupportForTests
{
    public abstract class WithInProcEventStoreAndNServiceBusPublisher
    {
        protected IDomainRepository CreateDomainRepository(IBus bus)
        {
            var eventMappings = new NServiceBusDomainEventMappingFactory().CreateMappingCollection();
            var eventPersistence = new InProcEventPersistence();
            var eventPublisher = new NServiceBusEventPublisher(bus, eventMappings);
            var eventStore = new EventStore(eventPersistence, eventPublisher);
            var domainRepository = new DomainRepository(eventStore);
            return domainRepository;
        }
    }
}