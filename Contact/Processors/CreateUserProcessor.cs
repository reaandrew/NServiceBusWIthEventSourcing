using System;
using Contact.Messages.Commands;
using Contact.Messages.Events;
using NServiceBus;

namespace Contact.Processors
{
    public class CreateUserProcessor : IHandleMessages<CreateUser>
    {
        public IBus Bus { get; set; }
        public void Handle(CreateUser message)
        {
            Console.WriteLine("User created");
            Bus.Publish(new UserCreated
                {
                    CorrelationId = message.CorrelationId,
                    Name = message.Name
                });
        }
    }
}
