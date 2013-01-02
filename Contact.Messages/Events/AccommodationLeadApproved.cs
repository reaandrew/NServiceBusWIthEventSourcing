using System;
using NServiceBus;

namespace Contact.Messages.Events
{
    public class AccommodationLeadApproved : IEvent
    {
        public Guid AccLeadId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}