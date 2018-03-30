using System;
using System.Security.Cryptography;
using System.Text;

namespace GlowGlider.Shared
{
    public static class TokenBuilder
    {
        public static string Build(params object[] args)
        {
            var input = string.Join("-", args);

            return CalculateHash(input);
        }

        private static string CalculateHash(string input)
        {
            using (var algorithm = SHA512.Create())
            {
                var hashedBytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

                return BitConverter.ToString(hashedBytes)
                    .Replace("-", "")
                    .ToLower()
                    .Substring(0, 16);
            }
        }

        public static string TokenFor(PublishRequest request)
        {
            return Build(request.PlayerId, request.PlayerAlias, request.Score);
        }
    }
}