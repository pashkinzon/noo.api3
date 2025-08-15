using Noo.Api.Core.Utils;

namespace Noo.UnitTests.Core.Utils;

public class RandomGeneratorTests
{
    [Fact]
    public void GenerateReadableCode_DefaultLength_IsAlphanumeric()
    {
        var code = RandomGenerator.GenerateReadableCode();
        Assert.Equal(6, code.Length);
        Assert.Matches("^[A-Z0-9]+$", code);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(32)]
    public void GenerateReadableCode_CustomLength(int length)
    {
        var code = RandomGenerator.GenerateReadableCode(length);
        Assert.Equal(length, code.Length);
        Assert.Matches("^[A-Z0-9]+$", code);
    }
}
