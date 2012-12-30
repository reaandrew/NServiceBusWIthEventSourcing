using System.Linq;
using System.Transactions;
using NServiceBus;

namespace Contact.Query.SqlServer.Subscribers
{
    public class AccommodationLeadApproved : IHandleMessages<Messages.Events.AccommodationLeadApproved>
    {
        public void Handle(Messages.Events.AccommodationLeadApproved message)
        {
            using (var context = new ContactEntities())
            {
                var obj = context.AccommodationLeads.SingleOrDefault(x => x.AccommodationLeadId == message.AccLeadId);
                obj.Approved = true;
                context.SaveChanges();
            }
        }
    }
}
