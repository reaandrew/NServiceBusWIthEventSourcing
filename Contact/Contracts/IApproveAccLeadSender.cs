using Contact.Messages.Commands;

namespace Contact.Contracts
{
    public interface IApproveAccLeadSender
    {
        void Send(ApproveAccLead message);
    }
}