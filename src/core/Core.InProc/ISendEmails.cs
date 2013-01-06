namespace Core.InProc
{
    public interface ISendEmails
    {
        void SendEmail(string address, string email);
    }
}