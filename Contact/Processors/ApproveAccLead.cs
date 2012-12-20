using Contact.Domain;
using Core;
using Core.DomainServices;
using NServiceBus;

namespace Contact.Processors
{
    public class ApproveAccLead : IHandleMessages<Messages.Commands.ApproveAccLead>
    {
        private readonly IDomainRepository _domainRepository;

        public ApproveAccLead(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public ApproveAccLead()
        {
        }

        public void Handle(Messages.Commands.ApproveAccLead message)
        {
            var accommodationLead = _domainRepository.Get<AccommodationLead>(message.AccLeadId);
            accommodationLead.Approve();
            _domainRepository.Save(accommodationLead);
        }
    }
}