using System;
using Contact.Core;
using Contact.Messages.Commands;
using Contact.Messages.Events;
using NServiceBus;

namespace Contact.Processors
{
    public class ApproveAccLeadProcessor : IHandleMessages<ApproveAccLead>
    {
        private readonly IEventPublisher _eventPublisher;

        public ApproveAccLeadProcessor(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public void Handle(ApproveAccLead message)
        {
            Console.Out.WriteLine(@"AccLead approved for {0}", message.AccLeadId);
            _eventPublisher.Publish(new AccLeadApproved
                {
                    AccLeadId = message.AccLeadId,
                    Name = "Joe Blogs"
                });
        }
    }
}
