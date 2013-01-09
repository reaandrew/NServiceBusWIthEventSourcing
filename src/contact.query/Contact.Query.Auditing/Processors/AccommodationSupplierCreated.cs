using Contact.Query.Auditing.DataAccess;
using NServiceBus;

namespace Contact.Query.Auditing.Processors
{
    public class AccommodationSupplierCreated : IHandleMessages<Messages.Events.AccommodationSupplierCreated>
    {
        private readonly IAuditInformationRepository _auditInformationRepository;

        public AccommodationSupplierCreated(IAuditInformationRepository auditInformationRepository)
        {
            _auditInformationRepository = auditInformationRepository;
        }

        public void Handle(Messages.Events.AccommodationSupplierCreated message)
        {
            _auditInformationRepository.SaveMessage(_auditInformationRepository);
        }
    }
}