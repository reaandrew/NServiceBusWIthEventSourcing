using Contact.Query.Auditing.DataAccess;
using NServiceBus;

namespace Contact.Query.Auditing.Processors
{
    public class AuthenticationCreated : IHandleMessages<Messages.Events.AuthenticationCreated>
    {
        private readonly IAuditInformationRepository _auditInformationRepository;

        public AuthenticationCreated(IAuditInformationRepository auditInformationRepository)
        {
            _auditInformationRepository = auditInformationRepository;
        }

        public void Handle(Messages.Events.AuthenticationCreated message)
        {
            _auditInformationRepository.SaveMessage(message);
        }
    }
}