using System;
using Contact.Domain;
using Contact.IntegrationTests.ProcessorsTests.SupportForTests;
using Contact.Messages.Commands;
using Contact.Messages.Events;
using NServiceBus.Testing;
using NUnit.Framework;

namespace Contact.IntegrationTests.ProcessorsTests
{
    [TestFixture]
    public class TestApproveAccLead : WithInProcEventStoreAndNServiceBusPublisher
    {
        [Test]
        public void ShouldPublishAnAccommodationLeadApprovedEvent()
        {
            const string name = "joe";
            const string email = "test@test.com";

            var accLeadId = Guid.NewGuid();
            var accommodationLead = new AccommodationLead(accLeadId, name, email);

            Test.Initialize();
            Test.Handler(bus =>
                {
                    var domainRepository = CreateDomainRepository(bus);
                    domainRepository.Save(accommodationLead);
                    return new Processors.ApproveAccLead(domainRepository);
                })
                .ExpectPublish<AccommodationLeadApproved>(approved => approved.AccLeadId == accLeadId &&
                                                                      approved.Name == name &&
                                                                      approved.Email == email)
                .OnMessage<ApproveAccLead>(lead => { lead.AccLeadId = accLeadId; });
        }
    }
}