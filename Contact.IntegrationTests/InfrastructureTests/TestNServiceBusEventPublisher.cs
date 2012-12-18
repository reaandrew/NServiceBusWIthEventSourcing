using Contact.Infrastructure;
using Contact.Infrastructure.NServiceBus;
using Contact.IntegrationTests.TestClasses;
using NServiceBus;
using NUnit.Framework;
using Rhino.Mocks;

namespace Contact.IntegrationTests.InfrastructureTests
{
    [TestFixture]
    public class TestNServiceBusEventPublisher
    {
        [Test]
        public void ShouldPublishEvent()
        {
            var mockBus = MockRepository.GenerateMock<IBus>();
            var domainEventMappingCollection = new DomainEventGenericMappingCollection<IEvent>();
            domainEventMappingCollection.AddMapping(new EmptyDomainEventMapper());
            var eventPublisher = new NServiceBusEventPublisher(mockBus, domainEventMappingCollection);
            var domainEvent = new EmptyDomainEvent();
            eventPublisher.Publish(domainEvent);
            //Already tested that the correct event should be being published here where the 
            //DomainEventMappingTests
            mockBus.AssertWasCalled(x => x.Publish(Arg<IEvent>.Is.Anything));
        }
    }
}