using System;
using NServiceBus;

namespace Contact.Messages.Events
{
    public class AccommodationSupplierCreated : IEvent
    {
        public Guid AccommodationSupplierId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}