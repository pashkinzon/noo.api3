namespace Noo.Api.Core.Utils;

public static class RandomGenerator
{
    private static readonly Random _random = new();

    private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public static string GenerateReadableCode(int length = 6)
    {
        return new string(Enumerable.Repeat(_chars, length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }
}
