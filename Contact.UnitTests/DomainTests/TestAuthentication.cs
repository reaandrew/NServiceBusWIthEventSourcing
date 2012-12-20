using System;
using Contact.Domain;
using Contact.Domain.DomainEvents;
using NUnit.Framework;

namespace Contact.UnitTests.DomainTests
{
    [TestFixture]
    public class TestAuthentication
    {
        [TestFixture]
        public class TestCreatingAuthenication
        {
            private Authentication _authentication;
            private Guid _id;
            private string _email;
            private string _hashedPassword;

            [SetUp]
            public void Setup()
            {
                _id = Guid.NewGuid();
                _email = "test@test.com";
                _hashedPassword = "Hash";
                _authentication = new Authentication(_id, _email, _hashedPassword);
            }

            [Test]
            public void ShouldContainOnlyOneOutstandingEvent()
            {
                Assert.That(_authentication.OutstandingEvents.Count, Is.EqualTo(1));
            }

            [Test]
            public void ShouldContainAnAuthenticationCreatedEvent()
            {
                var @event = _authentication.OutstandingEvents[0];
                Assert.That(@event, Is.TypeOf<AuthenticationCreated>());
            }

            [Test]
            public void ShoudAssignAVersionOf1ToTheAuthenticationCreatedEvent()
            {
                
            }

            [Test]
            public void ShouldAssignAuthenticationIDToTheAuthenticationCreatedEvent()
            {
                var @event = _authentication.OutstandingEvents[0] as AuthenticationCreated;
                Assert.That(@event.ID, Is.EqualTo(_id));
            }

            [Test]
            public void ShouldAssignEmailToTheAuthenticationCreatedEvent()
            {
                var @event = _authentication.OutstandingEvents[0] as AuthenticationCreated;
                Assert.That(@event.Email, Is.EqualTo(_email));
            }

            [Test]
            public void ShouldAssignTheHashedPasswordToTheAuthenticationCreatedEvent()
            {
                var @event = _authentication.OutstandingEvents[0] as AuthenticationCreated;
                Assert.That(@event.HashedPassword, Is.EqualTo(_hashedPassword));
            }
        }
    }
}
