using System;
using Contact.Messages.Events;
using Contact.Messages.State;
using NServiceBus;
using NServiceBus.Saga;

namespace Contact.Processors
{
    /// <summary>
    ///     I think this will be the saga
    /// </summary>
    public class CreateAccSupplier :
        Saga<CreateAccSupplierSagaData>,
        IAmStartedByMessages<Messages.Commands.CreateAccSupplier>,
        IHandleMessages<UserCreated>
    {
        public void Handle(Messages.Commands.CreateAccSupplier message)
        {
            Console.WriteLine("{0},{1},{2}",
                              Data.Id,
                              Data.OriginalMessageId,
                              Data.Originator);
            Console.WriteLine("Creating the Acc Supplier");

            Data.Email = message.Email;
            Bus.Send(new Messages.Commands.CreateUser
                {
                    Name = message.Name,
                    Email = message.Email
                });
        }

        public void Handle(UserCreated message)
        {
            Console.WriteLine("Acc Supplier has now been created");
            Bus.Publish(new AccSupplierCreated());
        }

        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<UserCreated>(s => s.Email, m => m.Email);
        }
    }
}