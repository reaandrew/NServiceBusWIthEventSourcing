namespace Contact.Core
{
    public interface ISendEmails
    {
        void SendEmail(string address, string email);
    }
}