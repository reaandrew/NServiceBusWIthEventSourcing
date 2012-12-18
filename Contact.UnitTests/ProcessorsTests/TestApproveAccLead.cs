using System;
using Contact.Infrastructure.NServiceBus;
using Contact.Messages.Commands;
using NServiceBus.Testing;
using NUnit.Framework;

namespace Contact.UnitTests.ProcessorsTests
{
    [TestFixture]
    public class TestApproveAccLead
    {
        [Test]
        public void ShouldPublishAnAccommodationLeadApprovedEvent()
        {
            Assert.Inconclusive();
            /*
            var accLeadId = Guid.NewGuid();

            var mapperFactory = new NServiceBusDomainEventMappingFactory();
            var mapperCollection = mapperFactory.CreateMappingCollection();

            Test.Initialize();

            Test.Handler(bus => new Processors.ApproveAccLead(new NServiceBusEventPublisher(bus,mapperCollection)))
                .ExpectPublish<ApproveAccLead>(approved => approved.AccLeadId == accLeadId)
                .OnMessage<ApproveAccLead>(lead => { lead.AccLeadId = accLeadId; });
             * */
        }
    }
}