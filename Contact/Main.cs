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

        }

        public void Stop()
        {
        }
    }
}