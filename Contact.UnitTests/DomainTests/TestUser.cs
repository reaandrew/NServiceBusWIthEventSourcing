using System;
using Contact.Domain;
using NUnit.Framework;

namespace Contact.UnitTests.DomainTests
{
    [TestFixture]
    public class TestUser
    {
        [TestFixture]
        public class TestCreatingAUser
        {
            [SetUp]
            public void Setup()
            {
                _id = Guid.NewGuid();
                _name = "Joe Bloggs";
                _email = "joe.bloggs@test.com";
                _user = new User(_id, _name, _email);
            }

            private Guid _id;
            private string _name;
            private string _email;
            private User _user;

            [Test]
            public void ShouldAssignIdOfTheUserToEvent()
            {
                var @event = _user.OutstandingEvents[0] as UserCreated;
                Assert.That(@event.ID, Is.EqualTo(_id));
            }

            [Test]
            public void ShouldAssignTheEmailOfTheUserToTheEvent()
            {
                var @event = _user.OutstandingEvents[0] as UserCreated;
                Assert.That(@event.Email, Is.EqualTo(_email));
            }

            [Test]
            public void ShouldAssignTheNameOfTheUserToTheEvent()
            {
                var @event = _user.OutstandingEvents[0] as UserCreated;
                Assert.That(@event.Name, Is.EqualTo(_name));
            }

            [Test]
            public void ShouldContainAUserCreatedEvent()
            {
                DomainEvent @event = _user.OutstandingEvents[0];
                Assert.That(@event, Is.TypeOf<UserCreated>());
            }

            [Test]
            public void ShouldContainOnlyOneOutstandingEvent()
            {
                Assert.That(_user.OutstandingEvents.Count, Is.EqualTo(1));
            }
        }
    }
}