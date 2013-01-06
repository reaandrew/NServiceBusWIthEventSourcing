using System;
using Contact.IntegrationTests.ProcessorsTests.SupportForTests;
using Contact.Messages.Commands;
using Contact.Messages.Events;
using NServiceBus.Testing;
using NUnit.Framework;

namespace Contact.IntegrationTests.ProcessorsTests
{
    [TestFixture]
    public class TestCreateAccommodationLead : WithInProcEventStoreAndNServiceBusPublisher
    {
        [Test]
        public void ShouldPublishAnAccommodationLeadCreatedEvent()
        {
            var accLeadId = Guid.NewGuid();
            const string name = "Joe";
            const string email = "test@test.com";

            Test.Initialize();
            Test.Handler(bus =>
                {
                    var domainRepository = CreateDomainRepository(bus);
                    return new Processors.CreateAccommodationLead(domainRepository);
                })
                .ExpectPublish<AccommodationLeadCreated>
                (created => created.AccommodationLeadID == accLeadId &&
                            created.Name == name &&
                            created.Email == email)
                .OnMessage<CreateAccommodationLead>(lead =>
                    {
                        lead.AccommodationLeadID = accLeadId;
                        lead.Name = name;
                        lead.Email = email;
                    });
        }
    }
}