using System;
using Contact.Infrastructure;
using Contact.Messages.Commands;
using Contact.Messages.Events;
using Contact.Processors;
using NServiceBus.Testing;
using NUnit.Framework;
using ApproveAccLead = Contact.Processors.ApproveAccLead;

namespace Contact.UnitTests.Processors
{
    [TestFixture]
    public class TestApproveAccLead
    {
        [Test]
        public void ShouldPublishAnAccommodationLeadApprovedEvent()
        {
            var accLeadId = Guid.NewGuid();

            Test.Initialize();

            Test.Handler<ApproveAccLead>(bus => new ApproveAccLead(new EventPublisher(bus)))
                .ExpectPublish<AccLeadApproved>(approved => approved.AccLeadId == accLeadId)
                .OnMessage<Messages.Commands.ApproveAccLead>(lead => { lead.AccLeadId = accLeadId; });
        }
    }
}
