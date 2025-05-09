using System.Net;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using Noo.Api.Core.Utils.Richtext;
using Noo.Api.Works.DTO;
using Noo.Api.Works.Types;

namespace Noo.Api.IntegrationTests.Works;

public class WorkIntegrationTests : IClassFixture<NooWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly NooWebApplicationFactory _factory;
    private const string _baseUrl = "/api/work";

    public WorkIntegrationTests(NooWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact(DisplayName = "Create a work and try to get it by id")]
    public async Task CreateWorkAsync__ShouldReturnCreated()
    {
        // Arrange
        var createDto = new CreateWorkDTO()
        {
            Title = "Test Work",
            Type = WorkType.Test,
            Description = "Test Description",
            Tasks =
            [
                new CreateWorkTaskDTO()
                {
                    Type = WorkTaskType.Word,
                    Order = 1,
                    MaxScore = 10,
                    Content = RichTestFactory.Create("Test Content"),
                    RightAnswer = "Test Answer",
                    SolveHint = RichTestFactory.Create("Test Hint"),
                    Explanation = RichTestFactory.Create("Test Explanation"),
                    CheckStrategy = WorkTaskCheckStrategy.Manual,
                    ShowAnswerBeforeCheck = false,
                    CheckOneByOne = false
                }
            ]
        };

        var content = JsonSerializer.Serialize(createDto);
        var httpContent = new StringContent(content, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync(_baseUrl, httpContent);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var responseBody = await response.Content.ReadAsStringAsync();
        responseBody.Should().NotBeNullOrEmpty();

        var workId = JsonSerializer.Deserialize<Ulid>(responseBody);
        workId.Should().NotBeNull();

        var workResponse = await _client.GetAsync($"{_baseUrl}/{workId}");

        workResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var workContent = await workResponse.Content.ReadAsStringAsync();

        workContent.Should().Contain("Test Work");
        workContent.Should().Contain("Test Description");

        workResponse.Dispose();
        response.Dispose();

        // clean the db
        // TODO: Implement a method to clean the database after each test
        // await _factory.CleanDatabaseAsync();
    }

    [Fact(DisplayName = "Create a work with invalid data")]
    public async Task CreateWorkAsync__ShouldReturnBadRequest()
    {
        // Arrange
        var createDto = new CreateWorkDTO()
        {
            Title = "Test Work",
            Type = WorkType.Test,
            Description = "Test Description",
            Tasks =
            [
                new CreateWorkTaskDTO()
                {
                    Type = WorkTaskType.Word,
                    Order = 1,
                    MaxScore = 10,
                    Content = RichTestFactory.Create("Test Content"),
                    RightAnswer = "Test Answer",
                    SolveHint = RichTestFactory.Create("Test Hint"),
                    Explanation = RichTestFactory.Create("Test Explanation"),
                    CheckStrategy = WorkTaskCheckStrategy.Manual,
                    ShowAnswerBeforeCheck = false,
                    CheckOneByOne = false
                }
            ]
        };

        var content = JsonSerializer.Serialize(createDto);
        var httpContent = new StringContent(content, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync(_baseUrl, httpContent);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var responseBody = await response.Content.ReadAsStringAsync();
        responseBody.Should().NotBeNullOrEmpty();

        response.Dispose();
    }
}