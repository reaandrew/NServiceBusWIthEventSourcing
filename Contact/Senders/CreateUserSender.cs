using Contact.Core;
using Contact.Messages.Commands;
using NServiceBus;

namespace Contact.Senders
{
    /*
     * ICommandSender
     * IEventPublisher
     * 
     */

    public class CreateUserSender : ISendCommand<CreateUser>
    {
        private readonly IBus _bus;

        public CreateUserSender(IBus bus)
        {
            _bus = bus;
        }

        public void Send(CreateUser message)
        {
            _bus.Send("Contact", message);
        }
    }
}