using System;
using NServiceBus;

namespace Contact.Messages.Events
{
    public class AuthenticationCreated : IEvent
    {
        public Guid AuthenticationID { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
    }
}
