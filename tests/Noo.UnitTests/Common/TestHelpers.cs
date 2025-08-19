using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using Noo.Api.Core.Config.Env;
using Noo.Api.Core.DataAbstraction.Db;

namespace Noo.UnitTests.Common;

public static class TestHelpers
{
    public static NooDbContext CreateInMemoryDb(string? dbName = null)
    {
        dbName ??= Guid.NewGuid().ToString();
        var options = new DbContextOptionsBuilder<NooDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        var dbConfig = Options.Create(new DbConfig
        {
            User = "u",
            Password = "p",
            Host = "h",
            Port = "3306",
            Database = "d",
            CommandTimeout = 30,
            DefaultCharset = "utf8mb4",
            DefaultCollation = "utf8mb4_unicode_ci"
        });

        return new NooDbContext(dbConfig, options);
    }

    public static Mock<IUnitOfWork> CreateUowMock(NooDbContext ctx)
    {
        var mock = new Mock<IUnitOfWork>();
        mock.SetupGet(u => u.Context).Returns(ctx);
        mock.Setup(u => u.CommitAsync(It.IsAny<CancellationToken>()))
            .Returns((CancellationToken ct) => ctx.SaveChangesAsync(ct));
        mock.Setup(u => u.Rollback());
        mock.Setup(u => u.Dispose());
        return mock;
    }
}
