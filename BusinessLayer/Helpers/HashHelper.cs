using System.Security.Cryptography;
using System.Text;

namespace BeautySalonService.BusinessLayer.Helpers
{
    public static class HashHelper
    {
        public static string GetSoltedHash(string password, string solt)
            => GenerateHash(password) + GenerateHash(solt);

        private static string GenerateHash(string toHash)
        {
            var crypt = new SHA256Managed();
            string hash = string.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(toHash));

            foreach (var bit in crypto)
            {
                hash += bit.ToString("x2");
            }

            return hash;
        }
    }
}
