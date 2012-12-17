using NServiceBus;

namespace Contact.Messages.Events
{
    public class AccSupplierCreated : IEvent
    {
        public string Name { get; set; }
    }
}