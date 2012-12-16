using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;
using NServiceBus;

namespace Server
{
    public class ApproveAccLeadProcessor : IHandleMessages<ApproveAccLead>
    {
        public void Handle(ApproveAccLead message)
        {
            Console.Out.WriteLine(@"AccLead approved for {0}", message.AccLeadId);
        }
    }
}
