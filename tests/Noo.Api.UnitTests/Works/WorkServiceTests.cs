using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Exceptions.Http;
using Noo.Api.Core.Request;
using Noo.Api.Core.Utils.Richtext;
using Noo.Api.Works;
using Noo.Api.Works.DTO;
using Noo.Api.Works.Models;
using Noo.Api.Works.Services;
using Noo.Api.Works.Types;
using SystemTextJsonPatch.Operations;

namespace Noo.Api.UnitTests.Works;

public class WorkServiceTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IRepository<WorkModel>> _mockWorkRepo;
    private readonly Mock<WorkRepository> _mockSpecificWorkRepo;
    private readonly WorkService _workService;
    private readonly Mock<IMapper> _mockMapper;

    public WorkServiceTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockWorkRepo = new Mock<IRepository<WorkModel>>();
        _mockSpecificWorkRepo = new Mock<WorkRepository>();
        _mockMapper = new Mock<IMapper>();

        // Setup default repository behavior
        _mockUnitOfWork.Setup(uow => uow.GetRepository<WorkModel>())
            .Returns(_mockWorkRepo.Object);

        _mockUnitOfWork.Setup(uow => uow.GetSpecificRepository<WorkRepository, WorkModel>())
            .Returns(_mockSpecificWorkRepo.Object);

        _workService = new WorkService(
            _mockUnitOfWork.Object,
            _mockMapper.Object,
            new WorkSearchStrategy()
        );
    }

    [Fact(DisplayName = "CreateWorkAsync should add work and commit")]
    public async Task CreateWorkAsync_AddsWorkAndCommits()
    {
        // Arrange
        var createDto = new CreateWorkDTO()
        {
            Title = "Test Work",
            Type = WorkType.Test,
            Description = "Test Description",
            Tasks = [
                new CreateWorkTaskDTO() {
                    Type = WorkTaskType.Word,
                    Order = 1,
                    MaxScore = 10,
                    Content = RichTestFactory.Create("Test Content"),

					// optional fields
					RightAnswer = "Test Answer",
                    SolveHint = RichTestFactory.Create("Test Hint"),
                    Explanation = RichTestFactory.Create("Test Explanation"),
                    CheckStrategy = WorkTaskCheckStrategy.Manual,
                    ShowAnswerBeforeCheck = false,
                    CheckOneByOne = false
                }
            ]
        };
        var expectedId = Ulid.NewUlid();

        _mockWorkRepo.Setup(repo => repo.AddAsync(It.IsAny<WorkModel>()))
            .Callback<WorkModel>(m => m.Id = expectedId) // Simulate ID generation
            .Returns(Task.CompletedTask);

        // Act
        var result = await _workService.CreateWorkAsync(createDto);

        // Assert
        Assert.Equal(expectedId, result);
        _mockWorkRepo.Verify(repo => repo.AddAsync(It.IsAny<WorkModel>()), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.CommitAsync(default), Times.Once);
    }

    [Fact(DisplayName = "GetWorkAsync should return work when it exists")]
    public async Task GetWorkAsync_ReturnsWork_WhenExists()
    {
        // Arrange
        var workId = Ulid.NewUlid();
        var expectedWork = new WorkModel { Id = workId };
        var expectedDto = new WorkResponseDTO { Id = workId };

        _mockMapper.Setup(m => m.Map<WorkModel, WorkResponseDTO>(expectedWork))
            .Returns(expectedDto);

        _mockSpecificWorkRepo.Setup(repo => repo.GetWithTasksAsync(workId))
            .ReturnsAsync(expectedWork);

        // Act
        var result = await _workService.GetWorkAsync(workId);

        // Assert
        Assert.Equal(expectedDto, result);
        _mockSpecificWorkRepo.Verify(repo => repo.GetWithTasksAsync(workId), Times.Once);
        _mockMapper.Verify(m => m.Map<WorkModel, WorkResponseDTO>(expectedWork), Times.Once);
        _mockMapper.Verify(m => m.Map<WorkModel, WorkResponseDTO>(It.IsAny<WorkModel>()), Times.Once);
    }

    [Fact(DisplayName = "GetWorkAsync should return null when work is not found")]
    public async Task GetWorkAsync_ReturnsNull_WhenNotFound()
    {
        // Arrange
        var workId = Ulid.NewUlid();
        _mockSpecificWorkRepo.Setup(repo => repo.GetWithTasksAsync(workId))
            .ReturnsAsync((WorkModel?)null);

        // Act
        var result = await _workService.GetWorkAsync(workId);

        // Assert
        Assert.Null(result);
    }

    [Fact(DisplayName = "GetWorksAsync should return paginated results")]
    public async Task GetWorksAsync_ReturnsPaginatedResults()
    {
        // Arrange
        var criteria = new Criteria<WorkModel>();
        const int expectedTotal = 10;

        var expectedDtos = new List<WorkResponseDTO>
        { new(), new(), new(), new(), new(), new(), new(), new(), new(), new() };

        // setup mock configuration provider
        var configProvider = new Mock<IConfigurationProvider>();
        configProvider.Setup(p => p.CreateMapper())
            .Returns(_mockMapper.Object);
        _mockMapper.Setup(m => m.ConfigurationProvider)
            .Returns(configProvider.Object);

        _mockWorkRepo.Setup(repo => repo.SearchAsync<WorkResponseDTO>(criteria, It.IsAny<ISearchStrategy<WorkModel>>(), configProvider.Object))
            .ReturnsAsync(new SearchResult<WorkResponseDTO>(expectedDtos, expectedTotal));

        // Act
        var (items, total) = await _workService.GetWorksAsync(criteria);

        // Assert
        Assert.Equal(expectedDtos, items);
        Assert.Equal(expectedTotal, total);
        _mockWorkRepo.Verify(repo => repo.SearchAsync<WorkResponseDTO>(criteria, It.IsAny<ISearchStrategy<WorkModel>>(), configProvider.Object), Times.Once);
        _mockMapper.Verify(m => m.Map<WorkModel, WorkResponseDTO>(It.IsAny<WorkModel>()), Times.Exactly(expectedTotal));
    }

    [Fact(DisplayName = "UpdateWorkAsync should update and commit when valid")]
    public async Task UpdateWorkAsync_UpdatesAndCommits_WhenValid()
    {
        // Arrange
        var workId = Ulid.NewUlid();
        var existingWork = new WorkModel
        {
            Id = workId,
            Title = "Old Title",
            Description = "Old Description",
        };
        var patch = new PatchPayload<UpdateWorkDTO>([
            new Operation<UpdateWorkDTO>("replace", "/title", "Updated Title"),
            new Operation<UpdateWorkDTO>("replace", "/description", "Updated Description")
        ]);

        var modelState = new ModelStateDictionary();

        _mockWorkRepo.Setup(repo => repo.GetByIdAsync(workId))
            .ReturnsAsync(existingWork);

        _mockUnitOfWork.Setup(uow => uow.CommitAsync(default))
            .ReturnsAsync(1);

        _mockMapper.Setup(m => m.Map<UpdateWorkDTO>(existingWork))
            .Returns(new UpdateWorkDTO
            {
                Title = existingWork.Title,
                Description = existingWork.Description
            });


        // Act
        await _workService.UpdateWorkAsync(workId, patch, modelState);

        // Assert
        _mockUnitOfWork.Verify(uow => uow.CommitAsync(default), Times.Once);
        _mockWorkRepo.Verify(repo => repo.GetByIdAsync(workId), Times.Once);
        _mockWorkRepo.Verify(repo => repo.Update(It.IsAny<WorkModel>()), Times.Once);
        _mockMapper.Verify(m => m.Map<WorkModel, WorkResponseDTO>(It.IsAny<WorkModel>()), Times.Once);
        Assert.Equal("Updated Title", existingWork.Title);
        Assert.Equal("Updated Description", existingWork.Description);
        Assert.True(modelState.IsValid);
    }

    [Fact(DisplayName = "UpdateWorkAsync should throw NotFoundException when work is missing")]
    public async Task UpdateWorkAsync_ThrowsNotFound_WhenWorkMissing()
    {
        // Arrange
        var workId = Ulid.NewUlid();
        var patch = new PatchPayload<UpdateWorkDTO>([
            new Operation<UpdateWorkDTO>("replace", "/title", "Updated Title")
        ]);
        _mockWorkRepo.Setup(repo => repo.GetByIdAsync(workId))
            .ReturnsAsync((WorkModel?)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _workService.UpdateWorkAsync(workId, patch));
    }

    [Fact(DisplayName = "DeleteWorkAsync should delete and commit")]
    public async Task DeleteWorkAsync_DeletesAndCommits()
    {
        // Arrange
        var workId = Ulid.NewUlid();

        // Act
        await _workService.DeleteWorkAsync(workId);

        // Assert
        _mockWorkRepo.Verify(repo => repo.DeleteById(workId), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.CommitAsync(default), Times.Once);
    }
}
