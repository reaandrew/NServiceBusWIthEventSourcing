using System;
using Core.Domain;

namespace Contact.Domain.DomainEvents
{
    public class AccommodationLeadApproved : DomainEvent
    {
        public AccommodationLeadApproved(Guid id)
        {
            ID = id;
        }

        public Guid ID { get; private set; }
    }
}