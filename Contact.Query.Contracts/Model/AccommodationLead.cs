using System;

namespace Contact.Query.Contracts.Model
{
    public class AccommodationLead
    {
        public Guid AccommodationLeadId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Approved { get; set; }
    }
}