using System;

namespace Core.InProc
{
    public class RandomNumberPasswordGenerator : IGeneratePassword
    {
        public string GeneratePassword()
        {
            return new Random().Next(0, int.MaxValue).ToString();
        }
    }
}