using System;
using Contact.Core;
using Contact.Messages.Commands;
using Contact.Messages.Events;
using NServiceBus;
using log4net;

namespace Contact.Processors
{
    public class CreateUserProcessor : IHandleMessages<CreateUser>
    {
        private readonly IEventPublisher _eventPublisher;

        public CreateUserProcessor(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public void Handle(CreateUser message)
        {
            LogManager.GetLogger(typeof (CreateUserProcessor)).Debug("User Created :-)");
            Console.WriteLine("User created");
            _eventPublisher.Publish(new UserCreated
                {
                    CorrelationId = message.CorrelationId,
                    Name = message.Name
                });
        }
    }
}
