using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Noo.Api.Works.DTO;
using Noo.Api.Works.Filters;
using Noo.Api.Works.Models;
using Noo.Api.Works.Services;
using Noo.Api.Works.Types;
using Noo.UnitTests.Common;

namespace Noo.UnitTests.Works;

public class WorkServiceTests
{

	private static IMapper CreateMapper()
	{
		var config = new MapperConfiguration(cfg =>
		{
			cfg.AddProfile<WorkMapperProfile>();
			cfg.AddProfile<Noo.Api.Subjects.Models.SubjectMapperProfile>();
		});
		config.AssertConfigurationIsValid();
		return config.CreateMapper();
	}

	[Fact]
	public async Task CreateGetUpdateDelete_Work_Flow()
	{
		var dbName = Guid.NewGuid().ToString();
		using var context = TestHelpers.CreateInMemoryDb(dbName);
		var uow = TestHelpers.CreateUowMock(context).Object;
		var mapper = CreateMapper();
		var service = new WorkService(uow, mapper);

		// Create
		var create = new CreateWorkDTO
		{
			Title = "Test Work",
			Type = WorkType.Test,
			Description = "desc",
			SubjectId = Ulid.NewUlid(),
			Tasks =
			[
				new CreateWorkTaskDTO { Type = WorkTaskType.Word, Order = 0, MaxScore = 1, Content = new Noo.Api.Core.Utils.Richtext.Delta.DeltaRichText("{}") }
			]
		};

		var id = await service.CreateWorkAsync(create);
		Assert.NotEqual(default, id);

		// Get
		var fetched = await service.GetWorkAsync(id);
		Assert.NotNull(fetched);
		Assert.Equal("Test Work", fetched!.Title);
		Assert.Single(fetched.Tasks!);

		// Search
		var search = await service.GetWorksAsync(new WorkFilter { Page = 1, PerPage = 10 });
		Assert.Equal(1, search.Total);
		Assert.Single(search.Items);

		// Update (patch title)
		var patch = new SystemTextJsonPatch.JsonPatchDocument<UpdateWorkDTO>();
		patch.Replace(x => x.Title, "Updated Title");
		await service.UpdateWorkAsync(id, patch, new ModelStateDictionary());

		var updated = await service.GetWorkAsync(id);
		Assert.Equal("Updated Title", updated!.Title);

		// Delete in a fresh context to avoid tracking conflict
		using var deleteContext = TestHelpers.CreateInMemoryDb(dbName);
		var deleteUow = TestHelpers.CreateUowMock(deleteContext).Object;
		var deleteService = new WorkService(deleteUow, mapper);
		await deleteService.DeleteWorkAsync(id);
		using var verifyContext = TestHelpers.CreateInMemoryDb(dbName);
		var verifyUow = TestHelpers.CreateUowMock(verifyContext).Object;
		var verifyService = new WorkService(verifyUow, mapper);
		var afterDelete = await verifyService.GetWorkAsync(id);
		Assert.Null(afterDelete);
	}
}
