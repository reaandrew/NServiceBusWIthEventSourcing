using Contact.Query.Auditing.DataAccess;
using NServiceBus;

namespace Contact.Query.Auditing.Processors
{
    public class AccommodationLeadApproved : IHandleMessages<Contact.Messages.Events.AccommodationLeadApproved>
    {
        private readonly IAuditInformationRepository _auditInformationRepository;

        public AccommodationLeadApproved(IAuditInformationRepository auditInformationRepository)
        {
            _auditInformationRepository = auditInformationRepository;
        }

        public void Handle(Messages.Events.AccommodationLeadApproved message)
        {
            _auditInformationRepository.SaveMessage(message);
        }
    }
}
