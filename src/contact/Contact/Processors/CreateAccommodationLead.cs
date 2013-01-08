using Contact.Domain;
using Core;
using NServiceBus;
using log4net;

namespace Contact.Processors
{
    public class CreateAccommodationLead : IHandleMessages<Messages.Commands.CreateAccommodationLead>
    {
        private readonly IDomainRepository _domainRepository;

        public CreateAccommodationLead(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public void Handle(Messages.Commands.CreateAccommodationLead message)
        {
            var accommodationLead = new AccommodationLead
                (message.AccommodationLeadID, message.Name, message.Email);
            _domainRepository.Save(accommodationLead);
        }
    }
}