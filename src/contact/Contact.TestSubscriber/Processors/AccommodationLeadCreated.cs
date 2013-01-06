using Contact.Messages.Commands;
using NServiceBus;

namespace Contact.TestSubscriber.Processors
{
    public class AccommodationLeadCreated : IHandleMessages<Messages.Events.AccommodationLeadCreated>
    {
        private readonly IBus _bus;

        public AccommodationLeadCreated(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(Messages.Events.AccommodationLeadCreated message)
        {
            _bus.Send(new ApproveAccLead
                {
                    AccLeadId = message.AccommodationLeadID
                });
        }
    }
}