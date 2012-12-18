using Contact.Core;
using NServiceBus;

namespace Contact.Processors
{
    public class CreateUser : IHandleMessages<Messages.Commands.CreateUser>
    {
        private readonly IEventPublisher _eventPublisher;

        public CreateUser(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public void Handle(Messages.Commands.CreateUser message)
        {
            //LogManager.GetLogger(typeof (CreateUser)).Debug("User Created :-)");
            //Console.WriteLine("User created");
            //_eventPublisher.Publish(new UserCreated
            //    {
            //        CorrelationId = message.CorrelationId,
            //        Name = message.Name
            //    });
        }
    }
}