using Contact.Core;
using Contact.Domain;
using NServiceBus;

namespace Contact.Processors
{
    public class CreateUser : IHandleMessages<Messages.Commands.CreateUser>
    {
        private readonly IDomainRepository _domainRepository;

        public CreateUser(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public void Handle(Messages.Commands.CreateUser message)
        {
            var user = new User(message.UserId, message.Name, message.Email);
            _domainRepository.Save(user);
        }
    }
}