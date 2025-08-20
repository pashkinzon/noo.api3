using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Noo.Api.Polls.DTO;
using Noo.Api.Polls.Filters;
using Noo.Api.Polls.Models;
using Noo.Api.Polls.Services;
using Noo.Api.Polls.Types;
using Noo.Api.Core.Security.Authorization;
using Noo.UnitTests.Common;
using SystemTextJsonPatch;

namespace Noo.UnitTests.Polls;

public class PollServiceTests
{
    private sealed class TestCurrentUser : ICurrentUser
    {
        public TestCurrentUser(Ulid? userId, UserRoles? role = null, bool isAuthenticated = true)
        {
            UserId = userId;
            UserRole = role;
            IsAuthenticated = isAuthenticated;
        }

        public Ulid? UserId { get; }
        public UserRoles? UserRole { get; }
        public bool IsAuthenticated { get; }
        public bool IsInRole(params UserRoles[] role) => UserRole.HasValue && role.Contains(UserRole.Value);
    }
    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<Noo.Api.Polls.Models.PollMapperProfile>());
        config.AssertConfigurationIsValid();
        return config.CreateMapper();
    }

    [Fact]
    public async Task Create_Get_Search_Update_Delete_Poll_Flow()
    {
        var dbName = Guid.NewGuid().ToString();
        using var context = TestHelpers.CreateInMemoryDb(dbName);
        var uow = TestHelpers.CreateUowMock(context).Object;
        var mapper = CreateMapper();
        var service = new PollService(mapper, uow);

        // Create poll with one question
        var create = new CreatePollDTO
        {
            Title = "Satisfaction",
            Description = "Quick survey",
            IsActive = true,
            IsAuthRequired = false,
            Questions = new[]
            {
                new CreatePollQuestionDTO
                {
                    Title = "Rate our app",
                    Description = "1-5",
                    IsRequired = true,
                    Type = PollQuestionType.Rating,
                    Config = new PollQuestionConfig { Type = PollQuestionType.Rating, MinRating = 1, MaxRating = 5 }
                }
            }
        };

        var pollId = await service.CreatePollAsync(create);
        Assert.NotEqual(default, pollId);

        // Get
        var fetched = await service.GetPollAsync(pollId);
        Assert.NotNull(fetched);
        Assert.Equal("Satisfaction", fetched!.Title);
        Assert.Single(fetched.Questions);

        // Search
        var search = await service.GetPollsAsync(new PollFilter { Page = 1, PerPage = 10, Search = "satis" });
        Assert.Equal(1, search.Total);
        Assert.Single(search.Items);

        // Update title via patch
        var patch = new JsonPatchDocument<UpdatePollDTO>();
        patch.Replace(x => x.Title, "Updated Title");
        await service.UpdatePollAsync(pollId, patch, new ModelStateDictionary());

        var updated = await service.GetPollAsync(pollId);
        Assert.Equal("Updated Title", updated!.Title);

        // Delete in a fresh context to avoid tracking issues
        using var deleteContext = TestHelpers.CreateInMemoryDb(dbName);
        var deleteUow = TestHelpers.CreateUowMock(deleteContext).Object;
        var deleteService = new PollService(mapper, deleteUow);
        await deleteService.DeletePollAsync(pollId);

        using var verifyContext = TestHelpers.CreateInMemoryDb(dbName);
        var verifyUow = TestHelpers.CreateUowMock(verifyContext).Object;
        var verifyService = new PollService(mapper, verifyUow);
        var afterDelete = await verifyService.GetPollAsync(pollId);
        Assert.Null(afterDelete);
    }

    [Fact]
    public async Task Participate_Prevents_Duplicate_By_UserId_Or_ExternalId()
    {
        var dbName = Guid.NewGuid().ToString();
        using var context = TestHelpers.CreateInMemoryDb(dbName);
        var uow = TestHelpers.CreateUowMock(context).Object;
        var mapper = CreateMapper();
        var service = new PollService(mapper, uow);

        // Seed poll
        var poll = new PollModel { Title = "P", IsActive = true, IsAuthRequired = false };
        context.Add(poll);
        await context.SaveChangesAsync();

        var userId = Ulid.NewUlid();
        const string extId = "ext-42";

        // 1) By userId: create with a current user, then attempt duplicate with the same user
        var withUser = new PollService(mapper, uow, new TestCurrentUser(userId));
        await withUser.ParticipateAsync(poll.Id, new CreatePollParticipationDTO
        {
            UserType = ParticipatingUserType.AuthenticatedUser,
            UserExternalIdentifier = null
        });
        await Assert.ThrowsAsync<Noo.Api.Polls.Exceptions.UserAlreadyVotedException>(async () =>
        {
            await withUser.ParticipateAsync(poll.Id, new CreatePollParticipationDTO
            {
                UserType = ParticipatingUserType.AuthenticatedUser,
                UserExternalIdentifier = null
            });
        });

        // 2) By external id: create with ext id, then attempt duplicate with the same ext id
        await service.ParticipateAsync(poll.Id, new CreatePollParticipationDTO
        {
            UserType = ParticipatingUserType.TelegramUser,
            UserExternalIdentifier = extId
        });
        await Assert.ThrowsAsync<Noo.Api.Polls.Exceptions.UserAlreadyVotedException>(async () =>
        {
            await service.ParticipateAsync(poll.Id, new CreatePollParticipationDTO
            {
                UserType = ParticipatingUserType.TelegramUser,
                UserExternalIdentifier = extId
            });
        });
    }

    [Fact]
    public async Task UpdatePollAnswer_Patches_Value()
    {
        var dbName = Guid.NewGuid().ToString();
        using var context = TestHelpers.CreateInMemoryDb(dbName);
        var uow = TestHelpers.CreateUowMock(context).Object;
        var mapper = CreateMapper();
        var service = new PollService(mapper, uow);

        // Seed question + answer
        var poll = new PollModel { Title = "P", IsActive = true, IsAuthRequired = false };
        var q = new PollQuestionModel { Poll = poll, Title = "Q", IsRequired = true, Type = PollQuestionType.Text, Order = 0 };
        var a = new PollAnswerModel { PollQuestion = q, Value = new PollAnswerValue { Type = PollQuestionType.Text, Value = "old" } };
        context.Add(a);
        await context.SaveChangesAsync();

        var patch = new JsonPatchDocument<UpdatePollAnswerDTO>();
        patch.Replace(x => x.Value, new PollAnswerValue { Type = PollQuestionType.Text, Value = "new" });

        await service.UpdatePollAnswerAsync(a.Id, patch, new ModelStateDictionary());

        var again = await context.Set<PollAnswerModel>().FindAsync(a.Id);
        Assert.Equal("new", again!.Value.Value as string);
    }
}
