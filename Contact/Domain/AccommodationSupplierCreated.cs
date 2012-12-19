using System;
using Core.Domain;

namespace Contact.Domain
{
    public class AccommodationSupplierCreated : DomainEvent
    {
        public AccommodationSupplierCreated(Guid id, string name, string email)
        {
            ID = id;
            Name = name;
            Email = email;
        }
        public Guid ID { get; private set; }
        public string Name { get; private set; }
        public string Email { get; set; }
    }
}