using System;
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
    public class CreateAccSupplierProcessor : 
        Saga<CreateAccSupplierSagaData>,
        IAmStartedByMessages<CreateAccSupplier>,
        IHandleMessages<UserCreated>
    {
        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<UserCreated>(s => s.CorrelationId, m => m.CorrelationId);
        }

        public void Handle(CreateAccSupplier message)
        {
            Console.WriteLine("{0},{1},{2}",
                              Data.Id,
                              Data.OriginalMessageId,
                              Data.Originator);
            Console.WriteLine("Creating the Acc Supplier");

            this.Data.CorrelationId = Guid.NewGuid();
            Bus.Send("Contact",new CreateUser
                {
                    CorrelationId = this.Data.CorrelationId,
                    Name = message.Name
                });
        }

        public void Handle(UserCreated message)
        {
            Console.WriteLine("Acc Supplier has now been created");
        }
    }
}
