using System;
using NServiceBus;

namespace Contact.Messages.Events
{
    public class UserCreated : IEvent
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}