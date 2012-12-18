using System.Linq;
using Contact.Domain;
using NUnit.Framework;

namespace Contact.UnitTests.Domain
{
    [TestFixture]
    public class TestAccommodationLead
    {
        [TestFixture]
        public class TestCreatingAnAccommodationLead
        {
            private string _name;
            private string _email;
            private AccommodationLead _accLead;

            [SetUp]
            public void Setup()
            {
                _name = "Joe";
                _email = "test@test.com";
                _accLead = new AccommodationLead(_name, _email);
            }

            [Test]
            public void ShouldContainOnlyOneOutstandingEvent()
            {
                Assert.That(_accLead.OutstandingEvents.Count, Is.EqualTo(1));
            }

            [Test]
            public void ShouldContainAnAccommodationLeadCreatedEvent()
            {
                var @event = _accLead.OutstandingEvents[0];
                Assert.That(@event, Is.TypeOf<AccommodationLeadCreated>());
            }

            [Test]
            public void ShouldContainAVersionNumberOfOne()
            {
                var @event = _accLead.OutstandingEvents[0];
                Assert.That(@event.Version, Is.EqualTo(1));
            }

            [Test]
            public void ShouldAssignTheNameOfTheAccommodationLeadToTheEvent()
            {
                var @event = _accLead.OutstandingEvents[0] as AccommodationLeadCreated;
                Assert.That(@event.Name, Is.EqualTo(_name));
            }

            [Test]
            public void ShouldAssignTheEmailOfTheAccommodationLeadToTheEvent()
            {
                var @event = _accLead.OutstandingEvents[0] as AccommodationLeadCreated;
                Assert.That(@event.Email, Is.EqualTo(_email));
            }
        }

        [TestFixture]
        public class TestApprovingAnAccommodationLead
        {
            private string _name;
            private string _email;
            private AccommodationLead _accLead;

            [SetUp]
            public void Setup()
            {
                _name = "Joe";
                _email = "Email";
                _accLead = new AccommodationLead(_name, _email);
                _accLead.Approve();
            }

            /// <summary>
            /// Further tests would be required to ensure that operations specific
            /// to an approved AccommodationLead or a unapproved AccommodationLead
            /// work correctly.  At this time, the only requirement is that an
            /// AccommodationLeadApprovedEvent is created to the private state
            /// for _approved is really unnessary at this time.
            /// </summary>
            [Test]
            public void ShouldContainAnAccommodationLeadApprovedEvent()
            {
                var @eventCount = _accLead.OutstandingEvents.OfType<AccommodationLeadApproved>().Count();
                Assert.That(@eventCount, Is.EqualTo(1));
            }
        }

    }
}
