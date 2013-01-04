using Contact.Domain;
using Contact.DomainServices;
using Core;
using NServiceBus;

namespace Contact.Processors
{
    public class CreateAuthenticationWithGeneratedPassword :
        IHandleMessages<Messages.Commands.CreateAuthenticationWithGeneratedPassword>
    {
        private readonly IDomainRepository _domainRepository;
        private readonly IGeneratePassword _passwordGenerator;
        private readonly IHash _hasher;

        public CreateAuthenticationWithGeneratedPassword(IDomainRepository domainRepository,
                                                         IGeneratePassword passwordGenerator,
                                                         IHash hasher)
        {
            _domainRepository = domainRepository;
            _passwordGenerator = passwordGenerator;
            _hasher = hasher;
        }

        public void Handle(Messages.Commands.CreateAuthenticationWithGeneratedPassword message)
        {
            var generatedPassword = _passwordGenerator.GeneratePassword();
            var hashedPassword = _hasher.Hash(generatedPassword);
            var authentication = new Authentication(message.AuthID, message.Email, hashedPassword);
            _domainRepository.Save(authentication);
        }
    }
}