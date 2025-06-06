using Noo.Api.Statistics.DTO;

namespace Noo.Api.Statistics.Services;

public interface IStatisticsService
{
    public Task<StatisticsDTO> GetMentorStatisticsAsync(Ulid mentorId, DateTime? from, DateTime? to);
    public Task<StatisticsDTO> GetPlatformStatisticsAsync(DateTime? from = null, DateTime? to = null);
    public Task<StatisticsDTO> GetStudentStatisticsAsync(Ulid studentId, DateTime? from = null, DateTime? to = null);
}
