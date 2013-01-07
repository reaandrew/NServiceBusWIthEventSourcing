using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact.Query.Auditing.DataAccess;
using NServiceBus;

namespace Contact.Query.Auditing.Processors
{
    public class CreateAccommodationLead : IHandleMessages<Contact.Messages.Commands.CreateAccommodationLead>
    {
        private readonly IAuditInformationRepository _auditInformationRepository;

        public CreateAccommodationLead(IAuditInformationRepository auditInformationRepository)
        {
            _auditInformationRepository = auditInformationRepository;
        }

        public void Handle(Messages.Commands.CreateAccommodationLead message)
        {
            _auditInformationRepository.SaveMessage(message);
        }
    }
}
