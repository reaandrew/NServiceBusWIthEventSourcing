using System;
using Contact.Core;
using Contact.Messages.Commands;
using Contact.Messages.Events;
using Contact.Messages.State;
using NServiceBus;
using NServiceBus.Saga;

namespace Contact.Processors
{
    /// <summary>
    /// I think this will be the saga
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

        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<UserCreated>(s => s.CorrelationId, m => m.CorrelationId);
        }

        public void Handle(Messages.Commands.CreateAccSupplier message)
        {
            Console.WriteLine("{0},{1},{2}",
                              Data.Id,
                              Data.OriginalMessageId,
                              Data.Originator);
            Console.WriteLine("Creating the Acc Supplier");

            this.Data.CorrelationId = Guid.NewGuid();
            Bus.Send(new Messages.Commands.CreateUser
                {
                    CorrelationId = this.Data.CorrelationId,
                    Name = message.Name
                });
        }

        public void Handle(UserCreated message)
        {
            Console.WriteLine("Acc Supplier has now been created");
            Bus.Publish(new AccSupplierCreated());
        }
    }
}
