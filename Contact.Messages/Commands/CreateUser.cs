using System;
using NServiceBus;

namespace Contact.Messages.Commands
{
    public class CreateUser : ICommand
    {
        public Guid CorrelationId { get; set; }
        public string Name { get; set; }
    }
}