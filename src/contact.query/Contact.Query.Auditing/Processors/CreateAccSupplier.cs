using Contact.Query.Auditing.DataAccess;
using NServiceBus;

namespace Contact.Query.Auditing.Processors
{
    public class CreateAccSupplier : IHandleMessages<Messages.Commands.CreateAccSupplier>
    {
        private readonly IAuditInformationRepository _auditInformationRepository;

        public CreateAccSupplier(IAuditInformationRepository auditInformationRepository)
        {
            _auditInformationRepository = auditInformationRepository;
        }

        public void Handle(Messages.Commands.CreateAccSupplier message)
        {
            _auditInformationRepository.SaveMessage(message);
        }
    }
}