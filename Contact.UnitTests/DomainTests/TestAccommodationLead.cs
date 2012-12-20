using System;
using System.Linq;
using Contact.Domain;
using Core.Domain;
using NUnit.Framework;

namespace Contact.UnitTests.DomainTests
{
    [TestFixture]
    public class TestAccommodationLead
    {
        /// <summary>
        ///     This is really a test for the aggregate root and simply using
        ///     a derived class of it as a harness.
        /// </summary>
        [TestFixture]
        public class TestMarkingChangesAsCommitted
        {
            [Test]
            public void ShouldClearOutstandingEventsWhenChangesAreMarkedAsCommitted()
            {
                var accLead = new AccommodationLead(Guid.NewGuid(), String.Empty, String.Empty);
                accLead.MarkChangesAsCommitted();
                Assert.That(accLead.OutstandingEvents.Count, Is.EqualTo(0));
            }
        }

        /// <summary>
        /// Looks like resharper has re-ordered all my tests in alphabetical order
        /// after a clean up. oops
        /// </summary>
        [TestFixture]
        public class TestCreatingAnAccommodationLead
        {
            private Guid _id;
            private string _name;
            private string _email;
            private AccommodationLead _accLead;

            [SetUp]
            public void Setup()
            {
                _id = Guid.NewGuid();
                _name = "Joe";
                _email = "test@test.com";
                _accLead = new AccommodationLead(_id, _name, _email);
            }

            [Test]
            public void ShouldAssignTheEmailOfTheAccommodationLeadToTheEvent()
            {
                var @event = _accLead.OutstandingEvents[0] as AccommodationLeadCreated;
                Assert.That(@event.Email, Is.EqualTo(_email));
            }

            [Test]
            public void ShouldAssignTheIdOfTheAccommodationLeadToTheEvent()
            {
                var @event = _accLead.OutstandingEvents[0] as AccommodationLeadCreated;
                Assert.That(@event.ID, Is.EqualTo(_id));
            }

            [Test]
            public void ShouldAssignTheNameOfTheAccommodationLeadToTheEvent()
            {
                var @event = _accLead.OutstandingEvents[0] as AccommodationLeadCreated;
                Assert.That(@event.Name, Is.EqualTo(_name));
            }

            [Test]
            public void ShouldContainAVersionNumberOfOne()
            {
                DomainEvent @event = _accLead.OutstandingEvents[0];
                Assert.That(@event.Version, Is.EqualTo(1));
            }

            [Test]
            public void ShouldContainAnAccommodationLeadCreatedEvent()
            {
                DomainEvent @event = _accLead.OutstandingEvents[0];
                Assert.That(@event, Is.TypeOf<AccommodationLeadCreated>());
            }

            [Test]
            public void ShouldContainOnlyOneOutstandingEvent()
            {
                Assert.That(_accLead.OutstandingEvents.Count, Is.EqualTo(1));
            }
        }

        [TestFixture]
        public class TestApprovingAnAccommodationLead
        {
            [SetUp]
            public void Setup()
            {
                _id = Guid.NewGuid();
                _name = "Joe";
                _email = "UserID";
                _accLead = new AccommodationLead(_id, _name, _email);
                _accLead.MarkChangesAsCommitted();
                _accLead.Approve();
            }

            private Guid _id;
            private string _name;
            private string _email;
            private AccommodationLead _accLead;

            [Test]
            public void ShouldAssignTheIdOfTheAccommodationLeadToTheApprovedEvent()
            {
                var @event = _accLead.OutstandingEvents[0] as AccommodationLeadApproved;
                Assert.That(@event.ID, Is.EqualTo(_id));
            }

            /// <summary>
            ///     Further tests would be required to ensure that operations specific
            ///     to an approved AccommodationLead or a unapproved AccommodationLead
            ///     work correctly.  At this time, the only requirement is that an
            ///     AccommodationLeadApprovedEvent is created to the private state
            ///     for _approved is really unnessary at this time.
            /// </summary>
            [Test]
            public void ShouldContainAnAccommodationLeadApprovedEvent()
            {
                int @eventCount = _accLead.OutstandingEvents.OfType<AccommodationLeadApproved>().Count();
                Assert.That(@eventCount, Is.EqualTo(1));
            }
        }
    }
}