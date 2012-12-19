using System;
using System.Collections.Generic;

namespace Contact.Domain
{
    public class User : AggregateRoot
    {
        private string _email;
        private string _name;

        public User(IEnumerable<DomainEvent> domainEvents)
            : base(domainEvents)
        {
            
        }

        public User(Guid id, string name, string email)
        {
            ApplyChange(new UserCreated(id, name, email));
        }
    }
}