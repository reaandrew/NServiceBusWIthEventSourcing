using System;
using NServiceBus;

namespace Contact.Messages.Events
{
    public class AccLeadApproved : IEvent
    {
        public Guid AccLeadId { get; set; }
        public string Name { get; set; }
    }
}