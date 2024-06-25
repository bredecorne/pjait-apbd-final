using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace RevenueRecognitionSystem.Helpers;

public static class SecurityHelpers
{
    public static (string hashedPassword, string salt) GetHashedPasswordAndSalt(string password)
    {
        var salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            10000,
            256 / 8));

        return (hashed, Convert.ToBase64String(salt));
    }

    public static string GetHashedPasswordWithSalt(string password, string salt)
    {
        var saltBytes = Convert.FromBase64String(salt);

        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password,
            saltBytes,
            KeyDerivationPrf.HMACSHA256,
            10000,
            256 / 8));
    }

    public static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }

        return Convert.ToBase64String(randomNumber);
    }
}