using System;
using NServiceBus;

namespace Contact.Messages.Commands
{
    public class CreateAccSupplier : ICommand
    {
        public Guid AccommodationSupplierId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}