using System.Net;
using System.Text;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Noo.Api.Courses.DTO;
using Noo.Api.Courses.Models;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Response;
using Cysharp.Serialization.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Noo.Api.IntegrationTests.Courses;

// for testing purposes, need to change to the actual DTO
public record CreateCourseDTO
{
    [Required]
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    
    [Required]
    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;
}

// for testing purposes, need to change to the actual DTO
public record CourseDTO
{
    [JsonPropertyName("id")]
    public Ulid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}

public class CourseControllerIntegrationTests : IClassFixture<NooWebApplicationFactory>
{
	private readonly HttpClient _client;
	private readonly IServiceProvider _services;
	private const string _baseUrl = "/course";

	public CourseControllerIntegrationTests(NooWebApplicationFactory factory)
	{
		_client = factory.CreateClient();
		_services = factory.Services;
	}

	[Fact(DisplayName = "Get course by id when it exists")]
	public async Task GetCourseAsync_WhenCourseExists_ReturnsOkWithCourse()
	{
		var courseId = Ulid.NewUlid();
		var course = new CourseModel
		{
			Id = courseId,
			Name = "Test Course",
			Description = "Test Description"
		};

		using (var scope = _services.CreateScope())
		{
			var dbContext = scope.ServiceProvider.GetRequiredService<NooDbContext>();
			dbContext.GetDbSet<CourseModel>().Add(course);
			await dbContext.SaveChangesAsync();
		}

		var response = await _client.GetAsync($"{_baseUrl}/{courseId}");

		response.StatusCode.Should().Be(HttpStatusCode.OK);

		var responseBody = await response.Content.ReadAsStringAsync();
		responseBody.Should().NotBeNullOrEmpty();

		var result = JsonSerializer.Deserialize<ApiResponseDTO<CourseDTO>>(responseBody);
		result.Should().NotBeNull();
		result!.Data.Should().NotBeNull();

		response.Dispose();
	}

	[Fact(DisplayName = "Get course by id when it does not exist")]
	public async Task GetCourseAsync_WhenCourseDoesNotExist_ReturnsNotFound()
	{
		var nonExistentCourseId = Ulid.NewUlid();

		var response = await _client.GetAsync($"{_baseUrl}/{nonExistentCourseId}");

		response.StatusCode.Should().Be(HttpStatusCode.NotFound);
		response.Dispose();
	}

	[Fact(DisplayName = "Get course by id when unauthorized")]
	public async Task GetCourseAsync_WhenUnauthorized_ReturnsUnauthorized()
	{
		var courseId = Ulid.NewUlid();
		_client.DefaultRequestHeaders.Authorization = null;

		var response = await _client.GetAsync($"{_baseUrl}/{courseId}");

		response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
		response.Dispose();
	}

	[Fact(DisplayName = "Create course with valid data")]
	public async Task CreateCourseAsync_WithValidData_ReturnsCreated()
	{
		var createCourseDto = new CreateCourseDTO
		{
			Name = "New Test Course",
			Description = "New Test Description"
		};

		var content = JsonSerializer.Serialize(createCourseDto);
		var httpContent = new StringContent(content, Encoding.UTF8, "application/json");

		var response = await _client.PostAsync(_baseUrl, httpContent);

		response.StatusCode.Should().Be(HttpStatusCode.Created);

		var responseBody = await response.Content.ReadAsStringAsync();
		responseBody.Should().NotBeNullOrEmpty();

		var result = JsonSerializer.Deserialize<ApiResponseDTO<CourseDTO>>(responseBody);
		result.Should().NotBeNull();
		result!.Data.Should().NotBeNull();
		result.Data.Name.Should().Be(createCourseDto.Name);
		result.Data.Description.Should().Be(createCourseDto.Description);

		response.Dispose();
	}

	[Fact(DisplayName = "Create course with invalid data")]
	public async Task CreateCourseAsync_WithInvalidData_ReturnsBadRequest()
	{
		var invalidCourseDto = new CreateCourseDTO
		{
			Name = "",
			Description = "Test Description"
		};

		var content = JsonSerializer.Serialize(invalidCourseDto);
		var httpContent = new StringContent(content, Encoding.UTF8, "application/json");

		var response = await _client.PostAsync(_baseUrl, httpContent);

		response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
		response.Dispose();
	}

	[Fact(DisplayName = "Create course when unauthorized")]
	public async Task CreateCourseAsync_WhenUnauthorized_ReturnsUnauthorized()
	{
		var createCourseDto = new CreateCourseDTO
		{
			Name = "Test Course",
			Description = "Test Description"
		};

		var content = JsonSerializer.Serialize(createCourseDto);
		var httpContent = new StringContent(content, Encoding.UTF8, "application/json");

		_client.DefaultRequestHeaders.Authorization = null;

		var response = await _client.PostAsync(_baseUrl, httpContent);

		response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
		response.Dispose();
	}
}