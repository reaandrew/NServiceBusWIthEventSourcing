using System;
using Contact.Domain;
using Contact.Domain.DomainEvents;
using NUnit.Framework;

namespace Contact.UnitTests.DomainTests
{
    [TestFixture]
    public class TestAccommodationSupplier
    {
        [TestFixture]
        public class TestCreatingAnAccommodationSupplier
        {
            private Guid _id;
            private string _name;
            private string _email;
            private AccommodationSupplier _accommodationSupplier;

            [SetUp]
            public void Setup()
            {
                _id = Guid.NewGuid();
                _name = "Joe";
                _email = "test@test.com";
                _accommodationSupplier = new AccommodationSupplier(_id, _name, _email);
            }

            [Test]
            public void ShouldContainOnlyOneOutstandingEvent()
            {
                Assert.That(_accommodationSupplier.OutstandingEvents.Count, Is.EqualTo(1));
            }

            [Test]
            public void ShouldContainAnAccommodationSupplierCreatedEvent()
            {
                var @event = _accommodationSupplier.OutstandingEvents[0];
                Assert.That(@event, Is.TypeOf<AccommodationSupplierCreated>());
            }

            [Test]
            public void ShouldAssignIdOfTheAccommodationSupplierToEvent()
            {
                var @event = _accommodationSupplier.OutstandingEvents[0] as AccommodationSupplierCreated;
                Assert.That(@event.ID, Is.EqualTo(_id));
            }

            [Test]
            public void ShouldAssignNameOfTheAccommodationSupplierToEvent()
            {
                var @event = _accommodationSupplier.OutstandingEvents[0] as AccommodationSupplierCreated;
                Assert.That(@event.Name, Is.EqualTo(_name));
            }   
 
            [Test]
            public void ShouldAssignEmailOfTheAccommodationSupplierToEvent()
            {
                var @event = _accommodationSupplier.OutstandingEvents[0] as AccommodationSupplierCreated;
                Assert.That(@event.Email, Is.EqualTo(_email));
            }
        }
    }
}
