using System;
using Contact.Domain;
using Contact.IntegrationTests.ProcessorsTests.SupportForTests;
using Contact.Messages.Commands;
using NServiceBus.Testing;
using NUnit.Framework;
using Rhino.Mocks;
using AccommodationLeadApproved = Contact.Messages.Events.AccommodationLeadApproved;

namespace Contact.IntegrationTests.ProcessorsTests
{
    [TestFixture]
    public class TestApproveAccLead : WithInProcEventStoreAndNServiceBusPublisher
    {
        [Test]
        public void ShouldPublishAnAccommodationLeadApprovedEvent()
        {
            var accLeadId = Guid.NewGuid();
            var accommodationLead = new AccommodationLead(accLeadId, "joe", "test@test.com");

            Test.Initialize();
            Test.Handler(bus =>
                {
                    var domainRepository = CreateDomainRepository(bus);
                    domainRepository.Save(accommodationLead);
                    return new Processors.ApproveAccLead(domainRepository);
                })
                .ExpectPublish<AccommodationLeadApproved>(approved => approved.AccLeadId == accLeadId)
                .OnMessage<ApproveAccLead>(lead => { lead.AccLeadId = accLeadId; });
        }
    }
}