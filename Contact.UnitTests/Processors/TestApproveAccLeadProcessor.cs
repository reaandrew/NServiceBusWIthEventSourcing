using System;
using Contact.Infrastructure;
using Contact.Messages.Commands;
using Contact.Messages.Events;
using Contact.Processors;
using NServiceBus.Testing;
using NUnit.Framework;

namespace Contact.UnitTests.Processors
{
    [TestFixture]
    public class TestApproveAccLeadProcessor
    {
        [Test]
        public void ShouldPublishAnAccommodationLeadApprovedEvent()
        {
            var accLeadId = Guid.NewGuid();

            Test.Initialize();

            Test.Handler<ApproveAccLeadProcessor>(bus => new ApproveAccLeadProcessor(new EventPublisher(bus)))
                .ExpectPublish<AccLeadApproved>(approved => approved.AccLeadId == accLeadId)
                .OnMessage<ApproveAccLead>(lead => { lead.AccLeadId = accLeadId; });
        }
    }
}
