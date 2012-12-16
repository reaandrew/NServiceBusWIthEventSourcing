using Contact.Messages.Commands;
using NServiceBus;

namespace Contact.Contracts
{
    public interface ICreateUserSender
    {
        IBus Bus { get; set; }
        void Send(CreateUser message);
    }
}