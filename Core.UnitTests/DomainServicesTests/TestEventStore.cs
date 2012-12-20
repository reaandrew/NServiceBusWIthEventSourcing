using System;
using System.Collections.Generic;
using Core.Domain;
using Core.DomainServices;
using Core.UnitTests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;
using Is = Rhino.Mocks.Constraints.Is;

namespace Core.UnitTests.DomainServicesTests
{
    [TestFixture]
    public class TestEventStore
    {
        [SetUp]
        public void Setup()
        {
            _eventPersistence = MockRepository.GenerateMock<IEventPersistence>();
            _eventPublisher = MockRepository.GenerateMock<IEventPublisher>();
            _eventStore = new EventStore(_eventPersistence, _eventPublisher);
        }

        private IEventPersistence _eventPersistence;
        private IEventPublisher _eventPublisher;
        private EventStore _eventStore;


        [Test]
        public void ShouldPublishAllTheOutstandingEvents()
        {
            var id = Guid.NewGuid();
            var events = new List<DomainEvent>
                {
                    new EmptyDomainEvent(id)
                };
            _eventStore.SaveEvents(id, events);
            _eventPublisher.AssertWasCalled(x => x.Publish(Arg<DomainEvent>.Matches(Is.TypeOf<EmptyDomainEvent>())));
        }

        [Test]
        public void ShouldSaveAllTheOutstandingEventsToTheEventPersistence()
        {
            var id = Guid.NewGuid();
            var events = new List<DomainEvent>
                {
                    new EmptyDomainEvent(id)
                };
            _eventStore.SaveEvents(id, events);
            _eventPersistence.AssertWasCalled(x => x.Save(Arg<Guid>.Is.Anything, Arg<EmptyDomainEvent>.Is.Anything));
        }

        [Test]
        public void ShouldBeAbleToGetEventsForAnAggregateRootById()
        {
            var eventPublisher = MockRepository.GenerateMock<IEventPublisher>();
            var eventPersistence = MockRepository.GenerateMock<IEventPersistence>();
            var id = Guid.NewGuid();
             var events = new List<DomainEvent>
                {
                    new EmptyDomainEvent(id)
                        {
                            Version = 1
                        }
                };
             eventPersistence.Stub<IEventPersistence, IList<DomainEvent>>(x => x.GetEventsForAggregate<EmptyDomainObject>(id)).Return(events);

            var eventStore = new EventStore(eventPersistence, eventPublisher);
            var retrievedEvents = eventStore.GetEventsForAggregate<EmptyDomainObject>(id);
            Assert.That((object) retrievedEvents, NUnit.Framework.Is.EquivalentTo(events));
        }
    }
}