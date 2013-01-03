using Contact.Query.Contracts;
using Contact.Query.Contracts.Model;
using NServiceBus;
using log4net;

namespace Contact.Query.Subscribers
{
    public class AccommodationLeadCreated : IHandleMessages<Contact.Messages.Events.AccommodationLeadCreated>
    {
        private readonly IContactQueryRepository _repository;

        public AccommodationLeadCreated(IContactQueryRepository repository)
        {
            _repository = repository;
        }

        public void Handle(Messages.Events.AccommodationLeadCreated message)
        {
            LogManager.GetLogger(this.GetType()).Info("Receieved " + message.GetType().ToString());
            var accommodationLead = new AccommodationLead
            {
                AccommodationLeadId = message.AccommodationLeadID,
                Name = message.Name,
                Email = message.Email
            };
            _repository.Save(accommodationLead);
        }
    }
}
