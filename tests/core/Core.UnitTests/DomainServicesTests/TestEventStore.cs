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
            eventPersistence.Stub(x => x.GetEventsForAggregate<EmptyDomainObject>(id)).Return(events);

            var eventStore = new EventStore(eventPersistence, eventPublisher);
            var retrievedEvents = eventStore.GetEventsForAggregate<EmptyDomainObject>(id);
            Assert.That(retrievedEvents, NUnit.Framework.Is.EquivalentTo(events));
        }

        [Test]
        public void ShouldBeVersionAscedingOrder()
        {
            var eventPublisher = MockRepository.GenerateMock<IEventPublisher>();
            var eventPersistence = MockRepository.GenerateMock<IEventPersistence>();
            var id = Guid.NewGuid();
            var events = new List<DomainEvent>
                {
                    new EmptyDomainEvent(id)
                        {
                            Version = 3
                        },
                    new EmptyDomainEvent(id)
                        {
                            Version = 1
                        },
                    new EmptyDomainEvent(id)
                        {
                            Version = 4
                        },
                    new EmptyDomainEvent(id)
                        {
                            Version = 2
                        }
                };
            eventPersistence.Stub(x => x.GetEventsForAggregate<EmptyDomainObject>(id)).Return(events);

            var eventStore = new EventStore(eventPersistence, eventPublisher);
            var retrievedEvents = eventStore.GetEventsForAggregate<EmptyDomainObject>(id);
            Assert.That(retrievedEvents[0].Version, NUnit.Framework.Is.EqualTo(1));
            Assert.That(retrievedEvents[1].Version, NUnit.Framework.Is.EqualTo(2));
            Assert.That(retrievedEvents[2].Version, NUnit.Framework.Is.EqualTo(3));
            Assert.That(retrievedEvents[3].Version, NUnit.Framework.Is.EqualTo(4));
        }
    }
}