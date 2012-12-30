using System;
using System.Configuration;
using System.Data.SqlClient;
using NServiceBus;

namespace Contact.Query.SqlServer.Subscribers
{
    public class AccommodationLeadCreated : IHandleMessages<Contact.Messages.Events.AccommodationLeadCreated>
    {
        public void Handle(Messages.Events.AccommodationLeadCreated message)
        {
            using (var context = new ContactEntities())
            {
                var accommodationLead = new AccommodationLead
                    {
                        AccommodationLeadId = message.AccommodationLeadID,
                        Name = message.Name,
                        Email = message.Email
                    };
                context.AccommodationLeads.Add(accommodationLead);
                context.SaveChanges();
            }
        }
    }
}
