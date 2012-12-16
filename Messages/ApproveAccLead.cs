using System;
using NServiceBus;

namespace Messages
{
    public class ApproveAccLead : ICommand
    {
        public Guid AccLeadId { get; set; }
    }
}
