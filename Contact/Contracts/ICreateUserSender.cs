using Contact.Messages.Commands;
using NServiceBus;

namespace Contact.Contracts
{
    public interface ICreateUserSender
    {
        void Send(CreateUser message);
    }
}