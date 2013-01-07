using System;
using System.Globalization;
using Contact.Query.Auditing.DataAccess;
using Contact.Query.Auditing.DataObjects;
using MongoDB.Driver;
using NServiceBus;
using log4net;
using log4net.Repository.Hierarchy;

namespace Contact.Query.Auditing.Processors
{

    public class ApproveAccLead : IHandleMessages<Contact.Messages.Commands.ApproveAccLead>
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