using System;
using Contact.Messages.Events;
using NServiceBus;

namespace Contact.Subscribers
{
    /// <summary>
    ///     Look into correlation id
    /// </summary>
    public class CreateAccSupplier : IHandleMessages<AccommodationLeadApproved>
    {
        public IBus Bus { get; set; }

        public void Handle(AccommodationLeadApproved message)
        {
            Console.WriteLine("AccLead Approved");
            Bus.Send("Contact", new Messages.Commands.CreateAccSupplier
                {
                    AccommodationSupplierId = Guid.NewGuid(),
                    Name = message.Name,
                    Email = message.Email
                });
        }
    }
}