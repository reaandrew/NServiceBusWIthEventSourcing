using System.Data.SqlClient;
using NServiceBus;

namespace Contact.Query.SqlServer.Subscribers
{
    public class AccommodationSupplierCreated : IHandleMessages<Contact.Messages.Events.AccommodationSupplierCreated>
    {
        public void Handle(Messages.Events.AccommodationSupplierCreated message)
        {
            using (var context = new ContactEntities())
            {
                var accommodationSupplier = new AccommodationSupplier
                    {
                        AccommodationSupplierId = message.AccommodationSupplierId,
                        Name = message.Name,
                        Email = message.Email
                    };
                context.AccommodationSuppliers.Add(accommodationSupplier);
                context.SaveChanges();
            }
        }
    }
}
