using System;
using Contact.Infrastructure;
using Contact.Messages.Commands;
using Contact.Messages.Events;
using NServiceBus.Testing;
using NUnit.Framework;

namespace Contact.UnitTests.Processors
{
    [TestFixture]
    public class TestApproveAccLead
    {
        [Test]
        public void ShouldPublishAnAccommodationLeadApprovedEvent()
        {
            Guid accLeadId = Guid.NewGuid();

            Test.Initialize();

            Test.Handler(bus => new Contact.Processors.ApproveAccLead(new EventPublisher(bus)))
                .ExpectPublish<AccLeadApproved>(approved => approved.AccLeadId == accLeadId)
                .OnMessage<ApproveAccLead>(lead => { lead.AccLeadId = accLeadId; });
        }
    }
}