﻿using Contact.Messages.Events;
using Core.InProc;
using NServiceBus;

namespace Contact.Subscribers
{
    public class SendAccSupplierRegistrationEmail : IHandleMessages<AccommodationSupplierCreated>
    {
        private readonly ISendEmails _emailSender;

        public SendAccSupplierRegistrationEmail(ISendEmails emailSender)
        {
            _emailSender = emailSender;
        }

        public void Handle(AccommodationSupplierCreated message)
        {
            _emailSender.SendEmail("", "");
        }
    }
}