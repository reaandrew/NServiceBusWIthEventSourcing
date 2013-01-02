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
        IHandleMessages<Messages.Events.AuthenticationCreated>,
    IHandleMessages<UserCreated>
    {
        public void Handle(Messages.Commands.CreateAccSupplier message)
        {
            Console.WriteLine("{0},{1},{2}",
                              Data.Id,
                              Data.OriginalMessageId,
                              Data.Originator);
            Console.WriteLine("Creating the Acc Supplier");
            Data.AccommodationSupplierId = message.AccommodationSupplierId;
            Data.Name = message.Name;
            Data.Email = message.Email;
            Data.AuthenticationID = Guid.NewGuid();
            Data.UserID = Guid.NewGuid();

            Bus.Send(new Messages.Commands.CreateAuthenticationWithGeneratedPassword
                {
                    AuthID = Data.AuthenticationID,
                    Email = message.Email
                });
        }

        public void Handle(AuthenticationCreated message)
        {
            Bus.Send(new Messages.Commands.CreateUser
            {
                UserId = Data.UserID,
                Name = Data.Name,
                Email = Data.Email
            });
        }

        public void Handle(UserCreated message)
        {
            Console.WriteLine("Acc Supplier has now been created");
            Bus.Publish(new AccommodationSupplierCreated
                {
                    Name = Data.Name,
                    Email = Data.Email
                });
        }

        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<UserCreated>(s => s.UserID, m => m.UserID);
            ConfigureMapping<AuthenticationCreated>(x => x.AuthenticationID, m => m.AuthenticationID);
        }
    }
}