using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact.Query.Auditing.DataAccess;
using NServiceBus;

namespace Contact.Query.Auditing.Processors
{
    public class CreateUser :
        IHandleMessages<Contact.Messages.Commands.CreateUser>
    {
        private readonly IAuditInformationRepository _auditInformationRepository;

        public CreateUser(IAuditInformationRepository auditInformationRepository)
        {
            _auditInformationRepository = auditInformationRepository;
        }

        public void Handle(Messages.Commands.CreateUser message)
        {
            _auditInformationRepository.SaveMessage(message);
        }
    }
}
