using System;
using Core.Domain;

namespace Contact.Domain.DomainEvents
{
    public class AccommodationLeadCreated : DomainEvent
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public Guid ID { get; set; }
    }
}