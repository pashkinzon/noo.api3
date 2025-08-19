using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Noo.Api.Core.Exceptions.Http;
using Noo.Api.Core.Utils.Richtext.Delta;
using Noo.Api.Snippets.DTO;
using Noo.Api.Snippets.Models;
using Noo.Api.Snippets.Services;
using Noo.UnitTests.Common;

namespace Noo.UnitTests.Snippets;

public class SnippetServiceTests
{
    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<SnippetMapperProfile>();
        });
        config.AssertConfigurationIsValid();
        return config.CreateMapper();
    }

    [Fact]
    public async Task Create_List_Update_Delete_Snippet_Flow()
    {
        var dbName = Guid.NewGuid().ToString();
        using var context = TestHelpers.CreateInMemoryDb(dbName);
        var uow = TestHelpers.CreateUowMock(context).Object;
        var mapper = CreateMapper();
        var service = new SnippetService(uow, mapper);

        var userId = Ulid.NewUlid();

        // Create
        var create = new CreateSnippetDTO
        {
            Name = "My first snippet",
            Content = new DeltaRichText("{}")
        };
        await service.CreateSnippetAsync(userId, create);

        // List
        var list = await service.GetSnippetsAsync(userId);
        Assert.True(list.Total >= 1);
        Assert.NotEmpty(list.Items);
        var created = list.Items.First();
        Assert.Equal(userId, created.UserId);
        Assert.Equal("My first snippet", created.Name);

        // Update (patch name)
        var patch = new SystemTextJsonPatch.JsonPatchDocument<UpdateSnippetDTO>();
        patch.Replace(x => x.Name, "Updated name");
        await service.UpdateSnippetAsync(userId, created.Id, patch, new ModelStateDictionary());

        using var verifyContext = TestHelpers.CreateInMemoryDb(dbName);
        var verifyUow = TestHelpers.CreateUowMock(verifyContext).Object;
        var verifyService = new SnippetService(verifyUow, mapper);
        var afterUpdate = await verifyService.GetSnippetsAsync(userId);
        var updated = afterUpdate.Items.FirstOrDefault(x => x.Id == created.Id);
        Assert.NotNull(updated);
        Assert.Equal("Updated name", updated!.Name);

        // Delete
        await verifyService.DeleteSnippetAsync(userId, created.Id);

        using var finalContext = TestHelpers.CreateInMemoryDb(dbName);
        var finalUow = TestHelpers.CreateUowMock(finalContext).Object;
        var finalService = new SnippetService(finalUow, mapper);
        var finalList = await finalService.GetSnippetsAsync(userId);
        Assert.Equal(0, finalList.Total);
        Assert.Empty(finalList.Items);
    }

    [Fact]
    public async Task Update_With_Wrong_User_Should_Throw_NotFound()
    {
        var dbName = Guid.NewGuid().ToString();
        using var context = TestHelpers.CreateInMemoryDb(dbName);
        var uow = TestHelpers.CreateUowMock(context).Object;
        var service = new SnippetService(uow, CreateMapper());

        var ownerId = Ulid.NewUlid();
        var otherUser = Ulid.NewUlid();

        await service.CreateSnippetAsync(ownerId, new CreateSnippetDTO
        {
            Name = "Owner's snippet",
            Content = new DeltaRichText("{}")
        });

        var list = await service.GetSnippetsAsync(ownerId);
        var snippetId = list.Items.First().Id;

        var patch = new SystemTextJsonPatch.JsonPatchDocument<UpdateSnippetDTO>();
        patch.Replace(x => x.Name, "Hacked");

        await Assert.ThrowsAsync<NotFoundException>(() =>
            service.UpdateSnippetAsync(otherUser, snippetId, patch, new ModelStateDictionary()));
    }

    [Fact]
    public async Task Update_With_Invalid_Data_Should_Throw_BadRequest()
    {
        var dbName = Guid.NewGuid().ToString();
        using var context = TestHelpers.CreateInMemoryDb(dbName);
        var uow = TestHelpers.CreateUowMock(context).Object;
        var service = new SnippetService(uow, CreateMapper());

        var userId = Ulid.NewUlid();
        await service.CreateSnippetAsync(userId, new CreateSnippetDTO
        {
            Name = "Valid name",
            Content = new DeltaRichText("{}")
        });

        var list = await service.GetSnippetsAsync(userId);
        var snippetId = list.Items.First().Id;

        // Name length > 63 should violate [MaxLength(63)] on UpdateSnippetDTO
        var tooLong = new string('a', 64);
        var patch = new SystemTextJsonPatch.JsonPatchDocument<UpdateSnippetDTO>();
        patch.Replace(x => x.Name, tooLong);

        await Assert.ThrowsAsync<BadRequestException>(() =>
            service.UpdateSnippetAsync(userId, snippetId, patch, new ModelStateDictionary()));
    }

    [Fact]
    public async Task Delete_With_Wrong_User_Should_Throw_NotFound()
    {
        var dbName = Guid.NewGuid().ToString();
        using var context = TestHelpers.CreateInMemoryDb(dbName);
        var uow = TestHelpers.CreateUowMock(context).Object;
        var mapper = CreateMapper();
        var service = new SnippetService(uow, mapper);

        var ownerId = Ulid.NewUlid();
        var otherUser = Ulid.NewUlid();

        await service.CreateSnippetAsync(ownerId, new CreateSnippetDTO
        {
            Name = "To delete",
            Content = new DeltaRichText("{}")
        });

        var list = await service.GetSnippetsAsync(ownerId);
        var snippetId = list.Items.First().Id;

        await Assert.ThrowsAsync<NotFoundException>(() => service.DeleteSnippetAsync(otherUser, snippetId));
    }

    [Fact]
    public async Task GetSnippets_Is_Filtered_By_User()
    {
        var dbName = Guid.NewGuid().ToString();
        using var context = TestHelpers.CreateInMemoryDb(dbName);
        var uow = TestHelpers.CreateUowMock(context).Object;
        var service = new SnippetService(uow, CreateMapper());

        var userA = Ulid.NewUlid();
        var userB = Ulid.NewUlid();

        await service.CreateSnippetAsync(userA, new CreateSnippetDTO { Name = "A1", Content = new DeltaRichText("{}") });
        await service.CreateSnippetAsync(userB, new CreateSnippetDTO { Name = "B1", Content = new DeltaRichText("{}") });

        var listA = await service.GetSnippetsAsync(userA);
        var listB = await service.GetSnippetsAsync(userB);

        Assert.Single(listA.Items);
        Assert.All(listA.Items, i => Assert.Equal(userA, i.UserId));
        Assert.Single(listB.Items);
        Assert.All(listB.Items, i => Assert.Equal(userB, i.UserId));
    }
}
