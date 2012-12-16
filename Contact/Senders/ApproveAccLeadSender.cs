using Contact.Contracts;
using Contact.Messages.Commands;
using NServiceBus;

namespace Contact.Senders
{
    public class ApproveAccLeadSender : IApproveAccLeadSender
    {
        public IBus Bus { get; set; }

        public void Send(ApproveAccLead message)
        {
            Bus.Send(message);
        }
    }
}
