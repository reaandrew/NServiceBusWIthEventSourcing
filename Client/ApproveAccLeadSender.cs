using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;
using NServiceBus;

namespace Client
{
    public class ApproveAccLeadSender : IWantToRunAtStartup
    {
        public IBus Bus { get; set; }

        public void Run()
        {
            Bus.Send("Server", new ApproveAccLead() { AccLeadId = Guid.NewGuid()});
        }

        public void Stop()
        {

        }
    }
}
