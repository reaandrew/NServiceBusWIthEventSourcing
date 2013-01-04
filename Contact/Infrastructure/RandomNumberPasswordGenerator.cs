using System;
using Contact.DomainServices;

namespace Contact.Infrastructure
{
    public class RandomNumberPasswordGenerator : IGeneratePassword
    {
        public string GeneratePassword()
        {
            return new Random().Next(0, int.MaxValue).ToString();
        }
    }
}