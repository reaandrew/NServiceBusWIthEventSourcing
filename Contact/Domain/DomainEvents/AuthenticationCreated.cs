using System;
using Core.Domain;

namespace Contact.Domain.DomainEvents
{
    public class AuthenticationCreated : DomainEvent
    {
        public AuthenticationCreated(Guid id, string email, string hashedPassword)
        {
            ID = id;
            Email = email;
            HashedPassword = hashedPassword;
        }

        public Guid ID { get; private set; }

        public string Email { get; private set; }

        public string HashedPassword { get; private set; }
    }
}
