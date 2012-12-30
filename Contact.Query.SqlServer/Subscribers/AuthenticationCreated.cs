using System.Data.Entity.Validation;
using System.Data.SqlClient;
using NServiceBus;

namespace Contact.Query.SqlServer.Subscribers
{
    public class AuthenticationCreated : IHandleMessages<Contact.Messages.Events.AuthenticationCreated>
    {
        public void Handle(Messages.Events.AuthenticationCreated message)
        {
            using (var context = new ContactEntities())
            {
                var authentication = new Authentication
                    {
                        AuthenticationId = message.AuthenticationID,
                        Email = message.Email,
                        HashedPassword = message.HashedPassword
                    };
                context.Authentications.Add(authentication);
                context.SaveChanges();
            }
        }
    }
}
