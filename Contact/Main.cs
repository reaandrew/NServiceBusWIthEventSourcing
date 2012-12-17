using System;
using Contact.Messages.Commands;
using NServiceBus;

namespace Contact
{
    public class Main : IWantToRunAtStartup
    {
        public IBus Bus { get; set; }

        public void Run()
        {
            Bus.Send("Contact", new ApproveAccLead {AccLeadId = Guid.NewGuid()});
            Console.WriteLine("Sent the Approve Acc Lead command");
        }

        public void Stop()
        {
        }
    }
}