using Contact.Core;
using Contact.Domain;
using NServiceBus;

namespace Contact.Processors
{
    public class ApproveAccLead : IHandleMessages<Messages.Commands.ApproveAccLead>
    {
        private readonly IEventStore _eventStore;

        public ApproveAccLead(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public ApproveAccLead()
        {
        }

        public void Handle(Messages.Commands.ApproveAccLead message)
        {
            //Need to implement the GET functionality in the EventSourcing.
            //Tis is the only thing missing.  This is temporary below and will break future tests
            var accLead = new AccommodationLead(message.AccLeadId, "something", "anything");
            _eventStore.SaveEvents(accLead.ID, accLead.OutstandingEvents);
        }
    }
}