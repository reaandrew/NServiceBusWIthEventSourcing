using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace Contact.Query.SqlServer.Subscribers
{
    public class UserCreated : IHandleMessages<Contact.Messages.Events.UserCreated>
    {
 
        public void Handle(Messages.Events.UserCreated message)
        {
            using (var context = new ContactEntities())
            {
                var user = new User
                    {
                        UserId = message.UserID,
                        Name = message.Name,
                        Email = message.Email
                    };
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}
