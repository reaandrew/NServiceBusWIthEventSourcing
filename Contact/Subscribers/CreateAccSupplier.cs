using System;
using Contact.Messages.Events;
using NServiceBus;

namespace Contact.Subscribers
{
    /// <summary>
    ///     Look into correlation id
    /// </summary>
    public class CreateAccSupplier : IHandleMessages<AccLeadApproved>
    {
        public IBus Bus { get; set; }

        public void Handle(AccLeadApproved message)
        {
            Console.WriteLine("AccLead Approved");
            Bus.Send("Contact", new Messages.Commands.CreateAccSupplier());
        }
    }
}