using System;

namespace Contact.Domain
{
    public class User : AggregateRoot
    {
        private string _email;
        private string _name;

        public User(Guid id, string name, string email)
        {
            ApplyChange(new UserCreated(id, name, email));
        }
    }
}