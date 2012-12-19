using System;
using NServiceBus;

namespace Contact.Messages.Events
{
    public class UserCreated : IEvent
    {
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}