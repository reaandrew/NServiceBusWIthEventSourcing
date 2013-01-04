using System;

namespace Contact.Query.Contracts.Model
{
    public class User
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}