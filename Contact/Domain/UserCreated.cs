using System;

namespace Contact.Domain
{
    public class UserCreated : DomainEvent
    {
        public UserCreated(Guid id, string name, string email)
        {
            ID = id;
            Name = name;
            Email = email;
        }

        public Guid ID { get; private set; }

        public string Name { get; private set; }

        public string Email { get; private set; }
    }
}