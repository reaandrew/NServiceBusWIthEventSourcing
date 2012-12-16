using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact.Messages.Commands;
using NServiceBus;

namespace Contact.MessageClient
{
    public class MessageSender : IWantToRunAtStartup
    {
        public IBus Bus { get; set; }

        public void Run()
        {
            Console.WriteLine("Ready");
            Console.ReadLine();
            Bus.Send("Contact", new ApproveAccLead
                {
                    AccLeadId = Guid.NewGuid()
                });
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
