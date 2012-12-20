using System;
using Contact.Domain.DomainEvents;
using Contact.Infrastructure;
using Contact.Infrastructure.NServiceBus;
using Infrastructure.NServiceBus;
using NServiceBus;
using NUnit.Framework;

namespace Contact.IntegrationTests.InfrastructureTests
{
    /// <summary>
    ///     Took this a little further when I saw an opportunity to explore
    ///     Generic Co-variance :-
    /// 
    /// </summary>
    [TestFixture]
    public class TestDomainEventGenericMappingCollection
    {
        [Test]
        public void ShouldLocateAnExistingMapperUsingASuppliedDomainEventType()
        {
            var domainEventMappingCollection = new NServiceBusEventMappings();

            domainEventMappingCollection.AddMapper(
                new Infrastructure.NServiceBus.DomainEventMappers.AccommodationLeadApprovedMapper());

            var domainEvent = new AccommodationLeadApproved(Guid.NewGuid());

            var mappedEvent =
                domainEventMappingCollection.GetMappedObjectFor(domainEvent);

            Assert.That(mappedEvent, Is.TypeOf<Contact.Messages.Events.AccommodationLeadApproved>());
        }

        [Test]
        [ExpectedException(typeof (MapperNotFoundException))]
        public void ShouldThrowDomainEventMapperNotFoundException()
        {
            var domainEventMappingCollection = new NServiceBusEventMappings();
            var domainEvent = new AccommodationLeadApproved(Guid.NewGuid());
            domainEventMappingCollection.GetMappedObjectFor(domainEvent);
        }
    }
}