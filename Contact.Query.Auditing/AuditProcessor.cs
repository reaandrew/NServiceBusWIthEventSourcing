using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact.Messages.Commands;
using NServiceBus;

namespace Contact.Query.Auditing
{
    public class AuditProcessor : IHandleMessages<Contact.Messages.Commands.ApproveAccLead>,
        IHandleMessages<Contact.Messages.Commands.CreateAccSupplier>,
        IHandleMessages<Contact.Messages.Commands.CreateAccommodationLead>,
        IHandleMessages<Contact.Messages.Commands.CreateAuthenticationWithGeneratedPassword>,
        IHandleMessages<Contact.Messages.Commands.CreateUser>
    {
        public void Handle(ApproveAccLead message)
        {
            throw new NotImplementedException();
        }

        public void Handle(CreateAccSupplier message)
        {
            throw new NotImplementedException();
        }

        public void Handle(CreateAccommodationLead message)
        {
            throw new NotImplementedException();
        }

        public void Handle(CreateAuthenticationWithGeneratedPassword message)
        {
            throw new NotImplementedException();
        }

        public void Handle(CreateUser message)
        {
            throw new NotImplementedException();
        }
    }
}
