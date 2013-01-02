using Contact.Query.Contracts;
using Contact.Query.Contracts.Model;
using NServiceBus;

namespace Contact.Query.Subscribers
{
    public class AccommodationSupplierCreated : IHandleMessages<Contact.Messages.Events.AccommodationSupplierCreated>
    {
        private readonly IContactQueryRepository _repository;

        public AccommodationSupplierCreated(IContactQueryRepository repository)
        {
            _repository = repository;
        }

        public void Handle(Messages.Events.AccommodationSupplierCreated message)
        {
            var accommodationSupplier = new AccommodationSupplier
            {
                AccommodationSupplierId = message.AccommodationSupplierId,
                Name = message.Name,
                Email = message.Email
            };
            _repository.Save(accommodationSupplier);

        }
    }
}
