using AutoMapper;
using Moq;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Security.Authorization;
using Noo.Api.Courses.DTO;
using Noo.Api.Courses.Filters;
using Noo.Api.Courses.Models;
using Noo.Api.Courses.Services;
using Noo.UnitTests.Common;
using SystemTextJsonPatch;

namespace Noo.UnitTests.Courses;

public class CourseServiceTests
{
    private static IMapper CreateMapper()
    {
        var cfg = new MapperConfiguration(c =>
        {
            c.AddProfile(new CourseMapperProfile());
            c.AddProfile(new Noo.Api.Media.Models.MediaMapperProfile());
            c.AddProfile(new Noo.Api.Subjects.Models.SubjectMapperProfile());
            c.AddProfile(new Noo.Api.Works.Models.WorkMapperProfile());
            c.AddProfile(new Noo.Api.Users.Models.UserMapperProfile());
        });
        cfg.AssertConfigurationIsValid();
        return cfg.CreateMapper();
    }

    private static ICurrentUser MakeUser(UserRoles role)
    {
        var mock = new Mock<ICurrentUser>();
        mock.SetupGet(m => m.UserId).Returns(Ulid.NewUlid());
        mock.SetupGet(m => m.UserRole).Returns(role);
        mock.SetupGet(m => m.IsAuthenticated).Returns(true);
        mock.Setup(m => m.IsInRole(It.IsAny<UserRoles[]>())).Returns<UserRoles[]>(roles => roles.Contains(role));
        return mock.Object;
    }

    private static CreateCourseDTO MakeCreateCourseDto(string name = "C# 101") => new()
    {
        Name = name,
        Description = "intro",
        SubjectId = Ulid.NewUlid(),
        StartDate = DateTime.UtcNow.Date,
        EndDate = DateTime.UtcNow.Date.AddDays(30)
    };

    [Fact]
    public async Task Create_And_GetById_Works()
    {
        using var ctx = TestHelpers.CreateInMemoryDb();
        var uow = TestHelpers.CreateUowMock(ctx).Object;
        var service = new CourseService(uow, MakeUser(UserRoles.Admin), CreateMapper());

        var id = await service.CreateAsync(MakeCreateCourseDto());
        Assert.NotEqual(default, id);

        var fetched = await service.GetByIdAsync(id, includeInactive: true);
        Assert.NotNull(fetched);
        Assert.Equal("C# 101", fetched!.Name);
        Assert.False(fetched.IsDeleted);
    }

    [Fact]
    public async Task Search_Respects_UserRole_Specification()
    {
        using var ctx = TestHelpers.CreateInMemoryDb();
        var uow = TestHelpers.CreateUowMock(ctx).Object;

        // Seed couple of courses directly
        var c1 = new CourseModel { Name = "A", SubjectId = Ulid.NewUlid() };
        var c2 = new CourseModel { Name = "B", SubjectId = Ulid.NewUlid() };
        uow.Context.GetDbSet<CourseModel>().AddRange(c1, c2);
        await uow.CommitAsync();

        // Admin sees all
        var adminService = new CourseService(uow, MakeUser(UserRoles.Admin), CreateMapper());
        var adminSearch = await adminService.SearchAsync(new CourseFilter { Page = 1, PerPage = 10 });
        Assert.Equal(2, adminSearch.Total);

        // Student sees none unless membership exists
        var student = MakeUser(UserRoles.Student);
        var studentService = new CourseService(uow, student, CreateMapper());
        var studentSearch = await studentService.SearchAsync(new CourseFilter { Page = 1, PerPage = 10 });
        Assert.Equal(0, studentSearch.Total);
    }

    [Fact]
    public async Task SoftDelete_Sets_IsDeleted_True_WhenFound()
    {
        var dbName = Guid.NewGuid().ToString();
        using var ctx = TestHelpers.CreateInMemoryDb(dbName);
        var uow = TestHelpers.CreateUowMock(ctx).Object;
        var service = new CourseService(uow, MakeUser(UserRoles.Admin), CreateMapper());

        var id = await service.CreateAsync(MakeCreateCourseDto("ToDelete"));
        await service.SoftDeleteAsync(id);

        // Verify in fresh context to avoid tracking illusions
        using var verifyCtx = TestHelpers.CreateInMemoryDb(dbName);
        var verifyUow = TestHelpers.CreateUowMock(verifyCtx).Object;
        var course = await verifyUow.Context.GetDbSet<CourseModel>().FindAsync(id);
        Assert.NotNull(course);
        Assert.True(course!.IsDeleted);
    }

    [Fact]
    public async Task CreateMaterialContent_Maps_And_Persists()
    {
        using var ctx = TestHelpers.CreateInMemoryDb();
        var uow = TestHelpers.CreateUowMock(ctx).Object;
        var service = new CourseService(uow, MakeUser(UserRoles.Admin), CreateMapper());

        var dto = new CreateCourseMaterialContentDTO
        {
            Content = new Noo.Api.Core.Utils.Richtext.Delta.DeltaRichText(),
            IsWorkAvailable = true,
            WorkSolveDeadlineAt = DateTime.UtcNow.AddDays(1)
        };

        var contentId = await service.CreateMaterialContentAsync(dto);
        Assert.NotEqual(default, contentId);

        var fetched = await service.GetContentByIdAsync(contentId);
        Assert.NotNull(fetched);
        Assert.True(fetched!.IsWorkAvailable);
    }

    [Fact]
    public async Task Update_NotImplemented_Throws()
    {
        using var ctx = TestHelpers.CreateInMemoryDb();
        var uow = TestHelpers.CreateUowMock(ctx).Object;
        var service = new CourseService(uow, MakeUser(UserRoles.Admin), CreateMapper());

        var id = await service.CreateAsync(MakeCreateCourseDto());
        var patch = new JsonPatchDocument<UpdateCourseDTO>();
        patch.Replace(x => x.Description, "new");
        await Assert.ThrowsAsync<NotImplementedException>(() => service.UpdateAsync(id, patch));
    }
}
