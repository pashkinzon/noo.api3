using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using Noo.Api.Core.Config.Env;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Utils.Richtext.Delta;
using Noo.Api.Support.DTO;
using Noo.Api.Support.Models;
using Noo.Api.Support.Services;

namespace Noo.UnitTests.Support;

public class SupportServiceTests
{
    private static (SupportService svc, NooDbContext ctx) CreateService()
    {
        var dbOptions = new DbContextOptionsBuilder<NooDbContext>()
            .UseInMemoryDatabase(databaseName: $"support-tests-{Guid.NewGuid()}")
            .Options;

        var dbConfig = Options.Create(new DbConfig
        {
            User = "u",
            Password = "p",
            Host = "localhost",
            Port = "3306",
            Database = "noo_test",
            CommandTimeout = 30,
            DefaultCharset = "utf8mb4",
            DefaultCollation = "utf8mb4_general_ci"
        });

        var ctx = new NooDbContext(dbConfig, dbOptions);

        var uow = new Mock<IUnitOfWork>();
        uow.SetupGet(x => x.Context).Returns(ctx);
        uow.Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
            .Returns<CancellationToken>(_ => ctx.SaveChangesAsync());

        var mapperCfg = new MapperConfiguration(cfg => cfg.AddProfile(new SupportMapperProfile()));
        var mapper = mapperCfg.CreateMapper();

        var svc = new SupportService(uow.Object, mapper);
        return (svc, ctx);
    }

    [Fact]
    public async Task CreateCategoryAsync_CreatesAndReturnsId()
    {
        var (svc, ctx) = CreateService();

        var dto = new CreateSupportCategoryDTO
        {
            Name = "Getting Started",
            Order = 1,
            IsPinned = true,
            IsActive = true,
            ParentId = null
        };

        var id = await svc.CreateCategoryAsync(dto);

        var saved = await ctx.GetDbSet<SupportCategoryModel>().FindAsync(id);
        Assert.NotNull(saved);
        Assert.Equal("Getting Started", saved!.Name);
        Assert.True(saved.IsPinned);
        Assert.True(saved.IsActive);
        Assert.Equal(1, saved.Order);
        Assert.Null(saved.ParentId);
    }

    [Fact]
    public async Task CreateArticleAsync_CreatesAndReturnsId()
    {
        var (svc, ctx) = CreateService();

        // Seed category
        var cat = new SupportCategoryModel { Name = "FAQ", Order = 1, IsActive = true };
        ctx.Add(cat);
        await ctx.SaveChangesAsync();

        var dto = new CreateSupportArticleDTO
        {
            Title = "How to use Noo",
            Order = 1,
            Content = DeltaRichText.FromString("hello"),
            IsActive = true,
            CategoryId = cat.Id
        };

        var id = await svc.CreateArticleAsync(dto);

        var saved = await ctx.GetDbSet<SupportArticleModel>().FindAsync(id);
        Assert.NotNull(saved);
        Assert.Equal("How to use Noo", saved!.Title);
        Assert.Equal(cat.Id, saved.CategoryId);
        Assert.True(saved.IsActive);
        Assert.Equal(1, saved.Order);
        Assert.False(saved.Content.IsEmpty());
    }

    [Fact]
    public async Task DeleteCategoryAsync_RemovesEntity()
    {
        var (svc, ctx) = CreateService();
        var cat = new SupportCategoryModel { Name = "Old", Order = 10 };
        ctx.Add(cat);
        await ctx.SaveChangesAsync();

        await svc.DeleteCategoryAsync(cat.Id);

        var exists = await ctx.GetDbSet<SupportCategoryModel>().FindAsync(cat.Id);
        Assert.Null(exists);
    }

    [Fact]
    public async Task DeleteArticleAsync_RemovesEntity()
    {
        var (svc, ctx) = CreateService();
        var cat = new SupportCategoryModel { Name = "FAQ", Order = 1 };
        var art = new SupportArticleModel { Title = "TBD", Order = 1, Category = cat, CategoryId = cat.Id };
        ctx.AddRange(cat, art);
        await ctx.SaveChangesAsync();

        await svc.DeleteArticleAsync(art.Id);

        var exists = await ctx.GetDbSet<SupportArticleModel>().FindAsync(art.Id);
        Assert.Null(exists);
    }

    [Fact]
    public async Task GetArticleAsync_ReturnsArticle_WhenExists()
    {
        var (svc, ctx) = CreateService();
        var cat = new SupportCategoryModel { Name = "FAQ", Order = 1 };
        var art = new SupportArticleModel { Title = "Welcome", Order = 1, Category = cat, CategoryId = cat.Id };
        ctx.AddRange(cat, art);
        await ctx.SaveChangesAsync();

        var result = await svc.GetArticleAsync(art.Id);
        Assert.NotNull(result);
        Assert.Equal("Welcome", result!.Title);
    }

    [Fact]
    public async Task GetCategoryTreeAsync_ReturnsOnlyActiveRoots_WithChildren()
    {
        var (svc, ctx) = CreateService();
        var root1 = new SupportCategoryModel { Name = "Root A", Order = 1, IsActive = true };
        var child1 = new SupportCategoryModel { Name = "Child A1", Order = 1, IsActive = true, Parent = root1, ParentId = root1.Id };
        var root2 = new SupportCategoryModel { Name = "Root B", Order = 2, IsActive = false };
        ctx.AddRange(root1, child1, root2);
        await ctx.SaveChangesAsync();

        var tree = await svc.GetCategoryTreeAsync();
        var roots = tree.ToList();

        Assert.Single(roots);
        Assert.Equal("Root A", roots[0].Name);
        Assert.Single(roots[0].Children);
        Assert.Equal("Child A1", roots[0].Children.First().Name);
    }

    [Fact]
    public async Task UpdateArticleAsync_IsNotImplemented_Yet()
    {
        var (svc, _) = CreateService();
        await Assert.ThrowsAsync<NotImplementedException>(async () => await svc.UpdateArticleAsync(Ulid.NewUlid(), new SystemTextJsonPatch.JsonPatchDocument<UpdateSupportArticleDTO>()));
    }

    [Fact]
    public async Task UpdateCategoryAsync_IsNotImplemented_Yet()
    {
        var (svc, _) = CreateService();
        await Assert.ThrowsAsync<NotImplementedException>(async () => await svc.UpdateCategoryAsync(Ulid.NewUlid(), new SystemTextJsonPatch.JsonPatchDocument<UpdateSupportCategoryDTO>()));
    }
}
