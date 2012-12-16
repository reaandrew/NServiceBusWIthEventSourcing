using System;
using Contact.Messages.Commands;
using Contact.Messages.Events;
using NServiceBus;

namespace Contact.Processors
{
    public class ApproveAccLeadProcessor : IHandleMessages<ApproveAccLead>
    {
        public IBus Bus { get; set; }

        public void Handle(ApproveAccLead message)
        {
            Console.Out.WriteLine(@"AccLead approved for {0}", message.AccLeadId);
            Bus.Publish(new AccLeadApproved
                {
                    AccLeadId = message.AccLeadId,
                    Name = "Joe Blogs"
                });
        }
    }
}
