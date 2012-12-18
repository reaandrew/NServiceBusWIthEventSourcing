using System;
using NServiceBus;

namespace Contact.Messages.Events
{
    public class AccommodationLeadApproved : IEvent
    {
        public Guid AccLeadId { get; set; }
    }
}