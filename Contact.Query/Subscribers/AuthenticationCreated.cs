using Contact.Query.Contracts;
using Contact.Query.Contracts.Model;
using NServiceBus;

namespace Contact.Query.Subscribers
{
    public class AuthenticationCreated : IHandleMessages<Messages.Events.AuthenticationCreated>
    {
        private readonly IContactQueryRepository _repository;

        public AuthenticationCreated(IContactQueryRepository repository)
        {
            _repository = repository;
        }

        public void Handle(Messages.Events.AuthenticationCreated message)
        {
            var authentication = new Authentication
                {
                    AuthenticationId = message.AuthenticationID,
                    Email = message.Email,
                    HashedPassword = message.HashedPassword
                };
            _repository.Save(authentication);
        }
    }
}