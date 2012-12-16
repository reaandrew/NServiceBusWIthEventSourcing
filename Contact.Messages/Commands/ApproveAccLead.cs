using System;
using NServiceBus;

namespace Contact.Messages.Commands
{
    public class ApproveAccLead : ICommand
    {
        public Guid AccLeadId { get; set; }
    }
}