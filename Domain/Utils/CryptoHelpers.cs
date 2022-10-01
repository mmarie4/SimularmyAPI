using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Utils;

public static class CryptoHelpers
{
    public static void CheckPassword(string password, User user)
    {
        var passwordHash = HashUsingPbkdf2(password, user.PasswordSalt);

        if (user.PasswordHash != passwordHash)
        {
            throw new Exception($"Incorrect password");
        }
    }

    /// <summary>
    ///     Hash a password
    /// </summary>
    /// <param name="password"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    public static string HashUsingPbkdf2(string password, string salt)
    {
        using var bytes = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 10000, HashAlgorithmName.SHA256);
        var derivedRandomKey = bytes.GetBytes(32);
        var hash = Convert.ToBase64String(derivedRandomKey);
        return hash;
    }

    /// <summary>
    ///     Generate a random base64 string for salt
    /// </summary>
    /// <returns></returns>
    public static string GenerateSalt()
    {
        var randomBytes = new byte[64];
        using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
        {
            rngCryptoServiceProvider.GetNonZeroBytes(randomBytes);
        }
        return Convert.ToBase64String(randomBytes);
    }

    public static string GenerateToken(User user, string secret, string issuer, string audience)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

        var permClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var token = new JwtSecurityToken(issuer,
                                         audience,
                                         permClaims,
                                         signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
