using System;

namespace Contact.Messages.Commands
{
    public class CreateAuthenticationWithGeneratedPassword
    {
        public Guid AuthID { get; set; }
        public string Email { get; set; }
    }
}
