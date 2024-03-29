using System;
using Contact.Domain.DomainEvents;
using Contact.Infrastructure.NServiceBus.DomainEventMappers;
using Infrastructure.NServiceBus;
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
            var domainEventMappingCollection = new NServiceBusEventMappings();
            domainEventMappingCollection.AddMapper(new AccommodationLeadApprovedMapper());
            var eventPublisher = new NServiceBusEventPublisher(mockBus, domainEventMappingCollection);
            var domainEvent = new AccommodationLeadApproved
                {
                    ID = Guid.NewGuid()
                };
            eventPublisher.Publish(domainEvent);
            //Already tested that the correct event should be being published here where the 
            //DomainEventMappingTests
            mockBus.AssertWasCalled(x => x.Publish(Arg<IEvent>.Is.Anything));
        }
    }
}