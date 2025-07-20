/* using Microsoft.AspNetCore.Mvc;
using Moq;
using Noo.Api.Courses.DTO;
using Noo.Api.Courses.Services;
using Noo.Api.Courses;
using Noo.Api.Core.DataAbstraction.Db;
using Cysharp.Serialization.Json;
using Noo.Api.Core.Response;
using Xunit;

namespace Noo.Api.UnitTests.Courses;

public class CourseControllerTests
{
	private readonly Mock<ICourseService> _courseServiceMock;
	private readonly Mock<ICourseMembershipService> _courseMembershipServiceMock;
	private readonly CourseController _controller;

	public CourseControllerTests()
	{
		_courseServiceMock = new Mock<ICourseService>();
		_courseMembershipServiceMock = new Mock<ICourseMembershipService>();
		_controller = new CourseController(_courseServiceMock.Object, _courseMembershipServiceMock.Object);
	}

	[Fact]
	public async Task GetCourseAsync_WhenCourseExists_ReturnsOkWithCourse()
	{
		var courseId = Ulid.NewUlid();
		var expectedCourse = new CourseDTO();
		_courseServiceMock.Setup(x => x.GetByIdAsync(courseId, true))
			.ReturnsAsync(expectedCourse);
		var result = await _controller.GetCourseAsync(courseId);

		var okResult = Assert.IsType<OkObjectResult>(result);
		var response = Assert.IsType<ApiResponseDTO<CourseDTO>>(okResult.Value);
		Assert.Equal(expectedCourse, response.Data);
	}

	[Fact]
	public async Task GetCourseAsync_WhenCourseDoesNotExist_ReturnsNotFound()
	{
		var courseId = Ulid.NewUlid();
		_courseServiceMock.Setup(x => x.GetByIdAsync(courseId, true))
			.ReturnsAsync((CourseDTO?)null);

		var result = await _controller.GetCourseAsync(courseId);

		Assert.IsType<NotFoundResult>(result);
	}
} */