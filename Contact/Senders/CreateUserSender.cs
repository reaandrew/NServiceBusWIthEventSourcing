using Contact.Contracts;
using Contact.Messages.Commands;
using NServiceBus;

namespace Contact.Senders
{
    public class CreateUserSender : ICreateUserSender
    {
        public IBus Bus { get; set; }
        public void Send(CreateUser message)
        {
            Bus.Send("Contact", message);
        }
    }
}
