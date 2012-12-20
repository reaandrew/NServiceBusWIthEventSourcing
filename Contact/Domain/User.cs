using System;
using System.Collections.Generic;
using Contact.Domain.DomainEvents;
using Core.Domain;

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

        private void Handle(UserCreated @event)
        {
            this.ID = @event.ID;
            this._email = @event.Email;
            this._name = @event.Name;
        }
    }
}