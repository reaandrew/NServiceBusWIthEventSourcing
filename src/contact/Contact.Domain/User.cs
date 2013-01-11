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
            ApplyChange(new UserCreated
                {
                    ID = id,
                    Name = name,
                    Email = email
                });
        }

        private void Apply(UserCreated @event)
        {
            this.ID = @event.ID;
            this._email = @event.Email;
            this._name = @event.Name;
        }
    }
}