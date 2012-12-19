using System;
using System.Collections.Generic;
using Contact.Core;
using Contact.Domain;
using Contact.DomainServices;
using Contact.UnitTests.TestClasses;
using Core.Domain;
using Core.DomainServices;
using NUnit.Framework;
using Rhino.Mocks;

namespace Contact.UnitTests.DomainServicesTests
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
            Assert.That(aggregateRoot.OutstandingEvents.Count, Is.EqualTo(0));
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
            Assert.That((object) aggregateRoot.Version, Is.EqualTo(1));
        }
    }
}
