using System;

namespace Contact.Query.Model
{
    public class Authentication
    {
        public int Id { get; set; }
        public Guid AuthenticationId { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
    }
}
