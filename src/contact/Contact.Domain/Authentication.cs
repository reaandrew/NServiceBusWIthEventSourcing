using System;
using Contact.Domain.DomainEvents;
using Core.Domain;

namespace Contact.Domain
{
    public class Authentication : AggregateRoot
    {
        private string _email;
        private string _hashedPassword;

        public Authentication(Guid id, string email, string hashedPassword)
        {
            this.ApplyChange(new AuthenticationCreated
                {
                    ID = id,
                    Email = email,
                    HashedPassword = hashedPassword
                });
        }

        private void Apply(AuthenticationCreated authenticationCreated)
        {
            this.ID = authenticationCreated.ID;
            this._email = authenticationCreated.Email;
            this._hashedPassword = authenticationCreated.HashedPassword;
        }
    }
}