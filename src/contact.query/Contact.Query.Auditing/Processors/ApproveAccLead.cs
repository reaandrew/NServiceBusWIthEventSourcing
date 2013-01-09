using Contact.Query.Auditing.DataAccess;
using NServiceBus;

namespace Contact.Query.Auditing.Processors
{
    public class ApproveAccLead : IHandleMessages<Messages.Commands.ApproveAccLead>
    {
        private readonly IAuditInformationRepository _auditInformationRepository;

        public ApproveAccLead(IAuditInformationRepository auditInformationRepository)
        {
            _auditInformationRepository = auditInformationRepository;
        }

        public void Handle(Messages.Commands.ApproveAccLead message)
        {
            _auditInformationRepository.SaveMessage(message);
        }
    }
}