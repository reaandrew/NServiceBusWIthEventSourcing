using System;

namespace Contact.Query.Contracts.Model
{
    public class AccommodationSupplier
    {
        public Guid AccommodationSupplierId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}