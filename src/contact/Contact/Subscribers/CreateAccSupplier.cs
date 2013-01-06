using System;
using Contact.Messages.Events;
using NServiceBus;
using log4net;

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
            Bus.Send(new Messages.Commands.CreateAccSupplier
                {
                    AccommodationSupplierId = Guid.NewGuid(),
                    Name = message.Name,
                    Email = message.Email
                });
        }
    }
}