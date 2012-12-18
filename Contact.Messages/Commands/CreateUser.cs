using NServiceBus;

namespace Contact.Messages.Commands
{
    public class CreateUser : ICommand
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}