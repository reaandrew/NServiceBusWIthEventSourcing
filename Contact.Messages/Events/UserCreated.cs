using System;
using NServiceBus;

namespace Contact.Messages.Events
{
    public class UserCreated : IEvent
    {
        public Guid CorrelationId { get; set; }
        public string Name { get; set; }
    }
}