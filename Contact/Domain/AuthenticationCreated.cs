using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;

namespace Contact.Domain
{
    public class AuthenticationCreated : DomainEvent
    {
        public AuthenticationCreated(Guid id, string email, string hashedPassword)
        {
            ID = id;
            Email = email;
            HashedPassword = hashedPassword;
        }

        public Guid ID { get; private set; }

        public string Email { get; private set; }

        public string HashedPassword { get; private set; }
    }
}
