using System;
using Core.Domain;

namespace Contact.Domain
{
    public class AccommodationLeadCreated : DomainEvent
    {
        public AccommodationLeadCreated(Guid id, string name, string email)
        {
            ID = id;
            Name = name;
            Email = email;
        }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public Guid ID { get; private set; }
    }
}