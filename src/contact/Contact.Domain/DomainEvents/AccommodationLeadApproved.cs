using System;
using Core.Domain;

namespace Contact.Domain.DomainEvents
{
    public class AccommodationLeadApproved : DomainEvent
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}