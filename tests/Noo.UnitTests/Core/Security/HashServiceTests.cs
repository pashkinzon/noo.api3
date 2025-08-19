using Noo.Api.Core.Security;

namespace Noo.UnitTests.Core.Security;

public class HashServiceTests
{
    [Fact]
    public void Hash_IsDeterministicAndVerifiable()
    {
        var svc = new HashService();
        var hash1 = svc.Hash("abc");
        var hash2 = svc.Hash("abc");
        var hash3 = svc.Hash("abcd");

        Assert.Equal(hash1, hash2);
        Assert.NotEqual(hash1, hash3);
        Assert.True(svc.Verify("abc", hash1));
        Assert.False(svc.Verify("abc", hash3));
    }
}
