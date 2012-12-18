using System;
using System.Collections.Generic;
using Contact.Core;
using Contact.Domain;
using Contact.Infrastructure;
using Contact.IntegrationTests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;
using Is = Rhino.Mocks.Constraints.Is;

namespace Contact.IntegrationTests.InfrastructureTests
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
                    new EmptyDomainEvent()
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
                    new EmptyDomainEvent()
                };
            _eventStore.SaveEvents(id, events);
            _eventPersistence.AssertWasCalled(x => x.Save(Arg<Guid>.Is.Anything, Arg<EmptyDomainEvent>.Is.Anything));
        }
    }
}