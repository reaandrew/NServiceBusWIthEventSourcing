namespace Contact.DomainServices
{
    public interface ISendEmails
    {
        void SendEmail(string address, string email);
    }
}