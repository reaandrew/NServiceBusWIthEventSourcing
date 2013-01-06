using System;
using NServiceBus;

namespace Contact.Messages.Commands
{
    public class CreateAuthenticationWithGeneratedPassword : ICommand
    {
        public Guid AuthID { get; set; }
        public string Email { get; set; }
    }
}