using System;
using Core.Domain;

namespace Contact.Domain.DomainEvents
{
    public class AuthenticationCreated : DomainEvent
    {
        public Guid ID { get; set; }

        public string Email { get; set; }

        public string HashedPassword { get; set; }
    }
}