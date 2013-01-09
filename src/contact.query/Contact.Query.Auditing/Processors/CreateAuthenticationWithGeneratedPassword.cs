using Contact.Query.Auditing.DataAccess;
using NServiceBus;

namespace Contact.Query.Auditing.Processors
{
    public class CreateAuthenticationWithGeneratedPassword :
        IHandleMessages<Messages.Commands.CreateAuthenticationWithGeneratedPassword>
    {
        private readonly IAuditInformationRepository _auditInformationRepository;

        public CreateAuthenticationWithGeneratedPassword(IAuditInformationRepository auditInformationRepository)
        {
            _auditInformationRepository = auditInformationRepository;
        }

        public void Handle(Messages.Commands.CreateAuthenticationWithGeneratedPassword message)
        {
            _auditInformationRepository.SaveMessage(message);
        }
    }
}