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
        //Need to understand how to keep constructor injection for Sagas
        //which is also compatible for testing.  I think a fix for the NServiceBUs
        //source code simply needs a constructor for Saga which is found in Handler
        //that gives you access to a created mock bus instance you can use.  Wonder
        //what the reason is why it is not there.  Simply because it has not be done
        //or purposely not implemented for some reason I do not yet know.  Prob the latter
        //but hey ho.

        //private readonly ISendCommand<CreateUser> _createUserSender;

        //public CreateAccSupplier()
        //{

        //}

        //public CreateAccSupplier(ISendCommand<CreateUser> createUserSender)
        //{
        //    _createUserSender = createUserSender;
        //}

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