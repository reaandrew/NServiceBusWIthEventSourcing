using NServiceBus;

namespace Contact.Query.Subscribers
{
    public class AccommodationLeadApproved : IHandleMessages<Messages.Events.AccommodationLeadApproved>
    {
        private readonly IContactQueryRepository _repository;

        public AccommodationLeadApproved(IContactQueryRepository repository)
        {
            _repository = repository;
        }

        public void Handle(Messages.Events.AccommodationLeadApproved message)
        {
            var accommodationLead = _repository.GetAccommodationLeadById(message.AccLeadId);
            accommodationLead.Approved = true;
            _repository.Save(accommodationLead);
        }
    }
}
