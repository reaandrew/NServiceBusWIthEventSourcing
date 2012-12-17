using System;
using Contact.Core;
using Contact.Messages.Commands;
using Contact.Messages.Events;
using NServiceBus;

namespace Contact.Processors
{
    public class ApproveAccLead : IHandleMessages<Messages.Commands.ApproveAccLead>
    {
        private readonly IEventPublisher _eventPublisher;

        public ApproveAccLead(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public ApproveAccLead()
        {

        }

        public void Handle(Messages.Commands.ApproveAccLead message)
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
