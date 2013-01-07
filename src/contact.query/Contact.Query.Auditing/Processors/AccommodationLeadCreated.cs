using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact.Query.Auditing.DataAccess;
using NServiceBus;

namespace Contact.Query.Auditing.Processors
{
    public class AccommodationLeadCreated : IHandleMessages<Contact.Messages.Events.AccommodationLeadCreated>
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
