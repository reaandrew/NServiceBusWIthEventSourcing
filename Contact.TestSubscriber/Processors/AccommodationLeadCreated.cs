using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace Contact.TestSubscriber.Processors
{
    public class AccommodationLeadCreated : IHandleMessages<Contact.Messages.Events.AccommodationLeadCreated>
    {
        private readonly IBus _bus;

        public AccommodationLeadCreated(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(Messages.Events.AccommodationLeadCreated message)
        {
            _bus.Send(new Contact.Messages.Commands.ApproveAccLead
                {
                    AccLeadId = message.AccommodationLeadID
                });
        }
    }
}
