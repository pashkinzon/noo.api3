using Noo.Api.Core.Utils.DI;
using Noo.Api.Statistics.DTO;

namespace Noo.Api.Statistics.Services;

[RegisterScoped(typeof(IStatisticsService))]
public class StatisticsService : IStatisticsService
{
    public Task<StatisticsDTO> GetMentorStatisticsAsync(Ulid mentorId, DateTime? from, DateTime? to)
    {
        throw new NotImplementedException();
    }

    public Task<StatisticsDTO> GetPlatformStatisticsAsync(DateTime? from = null, DateTime? to = null)
    {
        throw new NotImplementedException();
    }

    public Task<StatisticsDTO> GetStudentStatisticsAsync(Ulid studentId, DateTime? from = null, DateTime? to = null)
    {
        throw new NotImplementedException();
    }
}
