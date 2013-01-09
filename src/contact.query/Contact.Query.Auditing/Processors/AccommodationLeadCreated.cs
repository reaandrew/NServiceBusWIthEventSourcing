using Contact.Query.Auditing.DataAccess;
using NServiceBus;

namespace Contact.Query.Auditing.Processors
{
    public class AccommodationLeadCreated : IHandleMessages<Messages.Events.AccommodationLeadCreated>
    {
        private readonly IAuditInformationRepository _auditInformationRepository;

        public AccommodationLeadCreated(IAuditInformationRepository auditInformationRepository)
        {
            _auditInformationRepository = auditInformationRepository;
        }

        public void Handle(Messages.Events.AccommodationLeadCreated message)
        {
            _auditInformationRepository.SaveMessage(message);
        }
    }
}