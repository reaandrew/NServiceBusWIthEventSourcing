using System;
using Contact.Query.Contracts.Model;
using Contact.Query.Subscribers;
using Contact.Query.UnitTests.Helpers;
using NUnit.Framework;
using Rhino.Mocks;

namespace Contact.Query.UnitTests
{
    [TestFixture]
    public class TestAccommodationLead : WithFakeContactQueryRepository
    {
        [Test]
        public void ShouldUpdateTheAccommodationLeadWithApprovedStatus()
        {
            var id = Guid.NewGuid();

            Repository.Stub(x => x.GetAccommodationLeadById(id)).Return(new AccommodationLead());
            Repository.Stub(x => x.Save(default(AccommodationLead))).IgnoreArguments()
                      .WhenCalled(args =>
                          {
                              var leadToSave = args.Arguments[0] as AccommodationLead;
                              Assert.That(leadToSave.Approved, Is.True);
                          });


            var handler = new AccommodationLeadApproved(Repository);
            var @event = new Messages.Events.AccommodationLeadApproved
                {
                    AccLeadId = id
                };
            handler.Handle(@event);
        }

        [Test]
        public void ShouldCreateANewAccommodationLead()
        {
            var id = Guid.NewGuid();
            const string name = "Something";
            const string email = "test@test.com";

            var handler = new AccommodationLeadCreated(Repository);
            var @event = new Messages.Events.AccommodationLeadCreated
                {
                    AccommodationLeadID = id,
                    Name = name,
                    Email = email
                };
            handler.Handle(@event);
            Repository.AssertWasCalled(x => x.Save(Arg<AccommodationLead>.Is.Anything));
        }
    }
}