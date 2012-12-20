using System;

namespace Contact.WebApi.Contracts.Queries
{
    public class AccommodationLead
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Approved { get; set; }
    }
}