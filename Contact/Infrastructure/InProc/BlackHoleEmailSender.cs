using Contact.DomainServices;

namespace Contact.Infrastructure.InProc
{
    public class BlackHoleEmailSender : ISendEmails
    {
        public void SendEmail(string address, string email)
        {
            //Entered the event horizon...good film.
        }
    }
}
