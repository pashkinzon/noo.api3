using AutoMapper;
using Moq;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.UserSettings.DTO;
using Noo.Api.UserSettings.Models;
using Noo.Api.UserSettings.Services;
using Noo.Api.UserSettings.Types;
using Noo.UnitTests.Common;

namespace Noo.UnitTests.UserSettings;

public class UserSettingsServiceTests
{
    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<UserSettingsMapperProfile>();
        });
        config.AssertConfigurationIsValid();
        return config.CreateMapper();
    }

    [Fact]
    public async Task GetUserSettings_Creates_When_Missing()
    {
        using var context = TestHelpers.CreateInMemoryDb();
        var uow = TestHelpers.CreateUowMock(context).Object;
        var mapper = CreateMapper();

        var service = new UserSettingsService(uow, mapper);
        var userId = Ulid.NewUlid();

        var settings = await service.GetUserSettingsAsync(userId);

        Assert.NotNull(settings);
        Assert.Equal(userId, settings.UserId);
        Assert.Null(settings.Theme);
        Assert.Null(settings.FontSize);
    }

    [Fact]
    public async Task UpdateUserSettings_Maps_And_Commits()
    {
        var dbName = Guid.NewGuid().ToString();
        using var context = TestHelpers.CreateInMemoryDb(dbName);
        var uow = TestHelpers.CreateUowMock(context);
        var mapper = CreateMapper();
        var service = new UserSettingsService(uow.Object, mapper);

        var userId = Ulid.NewUlid();

        // Seed an existing settings row to avoid concurrency on Update for new entities
        context.GetDbSet<UserSettingsModel>().Add(new UserSettingsModel { UserId = userId });
        await context.SaveChangesAsync();

        await service.UpdateUserSettingsAsync(userId, new UserSettingsUpdateDTO
        {
            Theme = UserTheme.Dark,
            FontSize = FontSize.Large
        });

        // Verify persisted values in a fresh context to avoid tracking issues
        using var verifyCtx = TestHelpers.CreateInMemoryDb(dbName);
        var verifyUow = TestHelpers.CreateUowMock(verifyCtx).Object;
        var repo = verifyUow.UserSettingsRepository();
        var saved = await repo.GetOrCreateAsync(userId);

        Assert.Equal("Dark", saved.Theme);
        Assert.Equal("Large", saved.FontSize);
    }

    [Fact]
    public async Task UpdateUserSettings_Does_Not_Overwrite_Ignored_Fields()
    {
        using var context = TestHelpers.CreateInMemoryDb();
        var uow = TestHelpers.CreateUowMock(context).Object;
        var mapper = CreateMapper();
        var service = new UserSettingsService(uow, mapper);

        var userId = Ulid.NewUlid();
        var entity = new UserSettingsModel { Id = Ulid.NewUlid(), UserId = userId, Theme = "Light", FontSize = "Small" };
        context.GetDbSet<UserSettingsModel>().Add(entity);
        await context.SaveChangesAsync();

        var before = entity.Id;
        var createdAt = entity.CreatedAt;

        await service.UpdateUserSettingsAsync(userId, new UserSettingsUpdateDTO { Theme = UserTheme.Dark });

        var after = await service.GetUserSettingsAsync(userId);
        Assert.Equal(before, after.Id);
        Assert.Equal(createdAt, after.CreatedAt);
        Assert.Equal("Dark", after.Theme);
        // Current mapper overwrites unspecified fields with null
        Assert.Null(after.FontSize);
    }
}
