using System;
using Contact.Domain;
using Contact.Messages.Events;
using Contact.Messages.State;
using Core;
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
        IHandleMessages<AuthenticationCreated>,
        IHandleMessages<UserCreated>
    {
        public IDomainRepository DomainRepository { get; set; }

        public void Handle(Messages.Commands.CreateAccSupplier message)
        {
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
            var accommodationSupplier = new AccommodationSupplier(Data.AccommodationSupplierId, message.Name,
                                                                  message.Email);

            DomainRepository.Save(accommodationSupplier);
        }

        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<UserCreated>(s => s.UserID, m => m.UserID);
            ConfigureMapping<AuthenticationCreated>(x => x.AuthenticationID, m => m.AuthenticationID);
        }
    }
}