using Contact.Exceptions;
using Contact.Infrastructure;
using Contact.IntegrationTests.TestClasses;
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
            var domainEventMappingCollection = new DomainEventGenericMappingCollection<IEvent>();

            domainEventMappingCollection.AddMapping(new EmptyDomainEventMapper());

            var domainEvent = new EmptyDomainEvent();

            IEvent mappedEvent = domainEventMappingCollection.GetMappedEventFor(domainEvent);

            Assert.That(mappedEvent, Is.TypeOf<EmptyNServiceBusEvent>());
        }

        [Test]
        [ExpectedException(typeof (DomainEventNotFoundException))]
        public void ShouldThrowDomainEventMapperNotFoundException()
        {
            var domainEventMappingCollection = new DomainEventGenericMappingCollection<IEvent>();
            var domainEvent = new EmptyDomainEvent();
            domainEventMappingCollection.GetMappedEventFor(domainEvent);
        }
    }
}