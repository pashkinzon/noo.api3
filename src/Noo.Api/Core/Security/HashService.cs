using System.Security.Cryptography;
using System.Text;
using Noo.Api.Core.Utils.DI;

namespace Noo.Api.Core.Security;

[RegisterTransient(typeof(IHashService))]
public class HashService : IHashService
{
    private readonly HashAlgorithm _hashAlgorithm = SHA256.Create();

    public string Hash(string input)
    {
        var inputBytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = _hashAlgorithm.ComputeHash(inputBytes);

        return Convert.ToBase64String(hashBytes);
    }

    public bool Verify(string input, string hash)
    {
        return string.Equals(Hash(input), hash, StringComparison.Ordinal);
    }
}
