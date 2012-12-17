using Contact.Core;
using Contact.Messages.Commands;
using NServiceBus;

namespace Contact.Senders
{
    public class ApproveAccLeadSender : ISendCommand<ApproveAccLead>
    {
        public IBus Bus { get; set; }

        public void Send(ApproveAccLead message)
        {
            Bus.Send(message);
        }
    }
}