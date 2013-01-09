using Contact.Query.Auditing.DataAccess;
using NServiceBus;

namespace Contact.Query.Auditing.Processors
{
    public class UserCreated : IHandleMessages<Messages.Events.UserCreated>
    {
        private readonly IAuditInformationRepository _auditInformationRepository;

        public UserCreated(IAuditInformationRepository auditInformationRepository)
        {
            _auditInformationRepository = auditInformationRepository;
        }

        public void Handle(Messages.Events.UserCreated message)
        {
            _auditInformationRepository.SaveMessage(message);
        }
    }
}