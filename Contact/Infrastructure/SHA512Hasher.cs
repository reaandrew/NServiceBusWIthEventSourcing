using System;
using System.Security.Cryptography;
using System.Text;
using Contact.DomainServices;

namespace Contact.Infrastructure
{
    public class SHA512Hasher : IHash
    {
        public string Hash(string input)
        {
            var sha512Managed = new SHA512Managed();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hashBytes = sha512Managed.ComputeHash(inputBytes);
            var hash = BitConverter.ToString(hashBytes);
            return hash;
        }
    }
}