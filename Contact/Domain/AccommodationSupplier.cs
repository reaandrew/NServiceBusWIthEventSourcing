using System;
using System.Collections.Generic;
using Core.Domain;

namespace Contact.Domain
{
    public class AccommodationSupplier : AggregateRoot
    {
        private string _name;
        private string _email;

        public AccommodationSupplier(IEnumerable<DomainEvent> domainEvents)
            : base(domainEvents)
        {
            
        }

        public AccommodationSupplier(Guid id, string name, string email)
        {
            this.ApplyChange(new AccommodationSupplierCreated(id, name, email));
        }

        private void Apply(AccommodationSupplierCreated @event)
        {
            this.ID = @event.ID;
            this._name = @event.Name;
            this._email = @event.Email;
        }
    }
}