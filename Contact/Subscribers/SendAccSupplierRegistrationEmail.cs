using Contact.DomainServices;
using Contact.Messages.Events;
using NServiceBus;

namespace Contact.Subscribers
{
    public class SendAccSupplierRegistrationEmail : IHandleMessages<AccSupplierCreated>
    {
        private readonly ISendEmails _emailSender;

        public SendAccSupplierRegistrationEmail(ISendEmails emailSender)
        {
            _emailSender = emailSender;
        }

        public void Handle(AccSupplierCreated message)
        {
            _emailSender.SendEmail("", "");
        }
    }
}