using System;
using Contact.Domain.DomainEvents;
using Infrastructure.NServiceBus;
using NUnit.Framework;

namespace Contact.IntegrationTests.InfrastructureTests
{
    [TestFixture]
    public class TestDomainEventGenericMappingCollection
    {
        [Test]
        public void ShouldLocateAnExistingMapperUsingASuppliedDomainEventType()
        {
            var domainEventMappingCollection = new NServiceBusEventMappings();

            domainEventMappingCollection.AddMapper(
                new Infrastructure.NServiceBus.DomainEventMappers.AccommodationLeadApprovedMapper());

            var domainEvent = new AccommodationLeadApproved
                {
                    ID = Guid.NewGuid()
                };

            var mappedEvent =
                domainEventMappingCollection.GetMappedObjectFor(domainEvent);

            Assert.That(mappedEvent, Is.TypeOf<Contact.Messages.Events.AccommodationLeadApproved>());
        }

        [Test]
        [ExpectedException(typeof (MapperNotFoundException))]
        public void ShouldThrowDomainEventMapperNotFoundException()
        {
            var domainEventMappingCollection = new NServiceBusEventMappings();
            var domainEvent = new AccommodationLeadApproved
            {
                ID = Guid.NewGuid()
            };
            domainEventMappingCollection.GetMappedObjectFor(domainEvent);
        }
    }
}