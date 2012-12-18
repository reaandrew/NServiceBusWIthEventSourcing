using System;
using NServiceBus;

namespace Contact.Messages.Events
{
    public class AccommodationLeadCreated : IEvent
    {
        public Guid AccommodationLeadID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}