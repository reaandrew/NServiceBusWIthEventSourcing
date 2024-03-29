using Contact.Query.Contracts;
using Contact.Query.Contracts.Model;
using NServiceBus;

namespace Contact.Query.Subscribers
{
    public class UserCreated : IHandleMessages<Messages.Events.UserCreated>
    {
        private readonly IContactQueryRepository _repository;

        public UserCreated(IContactQueryRepository repository)
        {
            _repository = repository;
        }

        public void Handle(Messages.Events.UserCreated message)
        {
            var user = new User
                {
                    UserId = message.UserID,
                    Name = message.Name,
                    Email = message.Email
                };
            _repository.Save(user);
        }
    }
}