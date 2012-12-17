namespace Contact.Core
{
    public interface ISendCommand<T>
    {
        void Send(T command);
    }
}
