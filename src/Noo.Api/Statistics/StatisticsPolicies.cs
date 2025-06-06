using Microsoft.AspNetCore.Authorization;
using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.Statistics;

public class StatisticsPolicies : IPolicyRegistrar
{
    public const string CanGetPlatformStatistics = nameof(CanGetPlatformStatistics);
    public const string CanGetStudentStatistics = nameof(CanGetStudentStatistics);
    public const string CanGetMentorStatistics = nameof(CanGetMentorStatistics);

    public void RegisterPolicies(AuthorizationOptions options)
    {
        options.AddPolicy(CanGetPlatformStatistics, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Admin),
                nameof(UserRoles.Teacher)
            ).RequireNotBlocked();
        });

        options.AddPolicy(CanGetStudentStatistics, policy =>
        {
            // TODO: Change so that
            // - a student can access only his/her own statistics
            // - a mentor can access statistics of students he/she is mentoring
            // - an assistant, a teacher or an admin can access statistics of any student
            policy.RequireAuthenticatedUser().RequireNotBlocked();
        });

        options.AddPolicy(CanGetMentorStatistics, policy =>
        {
            // TODO: Change so that
            // - a mentor can access only his/her own statistics
            // - an assistant, a teacher or an admin can access statistics of any mentor
            // - a student can't access statistics of a mentor
            policy.RequireAuthenticatedUser().RequireNotBlocked();
        });
    }
}
