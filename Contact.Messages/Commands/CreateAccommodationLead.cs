using System;
using NServiceBus;

namespace Contact.Messages.Commands
{
    public class CreateAccommodationLead : ICommand
    {
        public Guid AccommodationLeadID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
