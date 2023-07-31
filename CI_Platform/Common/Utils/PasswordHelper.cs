using Common.Constants;
using System.Security.Cryptography;
using System.Text;

namespace Common.Utils;

public static class PasswordHelper
{
    private readonly static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

    public static string HashPassword(string password, out byte[] salt)
    {
        salt = RandomNumberGenerator.GetBytes(SystemConstant.KEY_SIZE);
        byte[] hash = GeneateHash(password, salt);

        return Convert.ToHexString(hash);
    }

    private static byte[] GeneateHash(string password, byte[] salt)
    {
        return Rfc2898DeriveBytes.Pbkdf2(
                    Encoding.UTF8.GetBytes(password), salt, SystemConstant.ITERATION_COUNT, hashAlgorithm, SystemConstant.KEY_SIZE);
    }

    public static bool VerifyPassword(string password, string hashedPassword, byte[] salt)
    {
        var hashToCompare = Rfc2898DeriveBytes
            .Pbkdf2(password, salt, SystemConstant.ITERATION_COUNT, hashAlgorithm, SystemConstant.KEY_SIZE);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hashedPassword));
    }
}