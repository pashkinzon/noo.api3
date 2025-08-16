using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Noo.Api.Subjects.DTO;
using Noo.Api.Subjects.Filters;
using Noo.Api.Subjects.Models;
using Noo.Api.Subjects.Services;
using Noo.UnitTests.Common;

namespace Noo.UnitTests.Subjects;

public class SubjectServiceTests
{

	private static IMapper CreateMapper()
	{
		var config = new MapperConfiguration(cfg => cfg.AddProfile<SubjectMapperProfile>());
		config.AssertConfigurationIsValid();
		return config.CreateMapper();
	}

	[Fact]
	public async Task CreateGetUpdateDelete_Subject_Flow()
	{
		var dbName = Guid.NewGuid().ToString();
		using var context = TestHelpers.CreateInMemoryDb(dbName);
		var uow = TestHelpers.CreateUowMock(context).Object;
		var mapper = CreateMapper();
		var service = new SubjectService(uow, mapper);

		var create = new SubjectCreationDTO { Name = "Biology", Color = "#abcdef" };
		var id = await service.CreateSubjectAsync(create);
		Assert.NotEqual(default, id);

		var fetched = await service.GetSubjectByIdAsync(id);
		Assert.NotNull(fetched);
		Assert.Equal("Biology", fetched!.Name);

		var search = await service.GetSubjectsAsync(new SubjectFilter { Page = 1, PerPage = 10 });
		Assert.Equal(1, search.Total);
		Assert.Single(search.Items);

		var patch = new SystemTextJsonPatch.JsonPatchDocument<SubjectUpdateDTO>();
		patch.Replace(x => x.Name, "Biology 2");
		await service.UpdateSubjectAsync(id, patch, new ModelStateDictionary());

		var updated = await service.GetSubjectByIdAsync(id);
		Assert.Equal("Biology 2", updated!.Name);

		using var deleteContext = TestHelpers.CreateInMemoryDb(dbName);
		var deleteUow = TestHelpers.CreateUowMock(deleteContext).Object;
		var deleteService = new SubjectService(deleteUow, mapper);
		await deleteService.DeleteSubjectAsync(id);

		using var verifyContext = TestHelpers.CreateInMemoryDb(dbName);
		var verifyUow = TestHelpers.CreateUowMock(verifyContext).Object;
		var verifyService = new SubjectService(verifyUow, mapper);
		var afterDelete = await verifyService.GetSubjectByIdAsync(id);
		Assert.Null(afterDelete);
	}
}
