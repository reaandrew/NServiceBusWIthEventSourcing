using System;
using System.Collections.Generic;
using Core.Domain;
using Core.DomainServices;
using Core.UnitTests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;

namespace Core.UnitTests.DomainServicesTests
{
    [TestFixture]
    public class TestDomainRepository
    {
        [Test]
        public void ShouldSaveAggregateRoot()
        {
            var id = Guid.NewGuid();
            var fakeEventStore = MockRepository.GenerateMock<IEventStore>();
            var aggregateRoot = new EmptyDomainObject(id);
            fakeEventStore.Expect(x => x.SaveEvents(id, aggregateRoot.OutstandingEvents));
            var repository = new DomainRepository(fakeEventStore);
            repository.Save(aggregateRoot);
            fakeEventStore.VerifyAllExpectations();
            Assert.That((object)aggregateRoot.OutstandingEvents.Count, Is.EqualTo(0));
        }

        [Test]
        public void ShouldGetAggregateRootById()
        {
            var id = Guid.NewGuid();
            var events = new List<DomainEvent>
                {
                    new EmptyDomainEvent(id)
                        {
                            Version = 1
                        }
                };
            var fakeEventStore = MockRepository.GenerateMock<IEventStore>();
            fakeEventStore.Stub<IEventStore, IList<DomainEvent>>(x => x.GetEventsForAggregate<EmptyDomainObject>(id)).Return(events);
            var repository = new DomainRepository(fakeEventStore);
            var aggregateRoot = repository.Get<EmptyDomainObject>(id);
            Assert.That((object)aggregateRoot.Version, Is.EqualTo(1));
        }

        [Test]
        [ExpectedException(ExpectedException = typeof(NullReferenceException),
            ExpectedMessage = "No events found for the ID of the Aggregate supplied")]
        public void ShouldThrowExceptionWhenNoDomainEventExistForAnAggregate()
        {
            var id = Guid.NewGuid();
            var events = new List<DomainEvent>();
            var fakeEventStore = MockRepository.GenerateMock<IEventStore>();
            fakeEventStore.Stub<IEventStore, IList<DomainEvent>>(x => x.GetEventsForAggregate<EmptyDomainObject>(id)).Return(events);
            var repository = new DomainRepository(fakeEventStore);
            var aggregateRoot = repository.Get<EmptyDomainObject>(id);
        }
    }
}
