using Microsoft.AspNetCore.Authorization;
using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.Polls;

public class PollPolicies : IPolicyRegistrar
{
    public const string CanCreatePoll = nameof(CanCreatePoll);
    public const string CanUpdatePoll = nameof(CanUpdatePoll);
    public const string CanDeletePoll = nameof(CanDeletePoll);
    public const string CanGetPolls = nameof(CanGetPolls);
    public const string CanGetPoll = nameof(CanGetPoll);
    public const string CanGetPollResults = nameof(CanGetPollResults);
    public const string CanGetPollResult = nameof(CanGetPollResult);
    public const string CanParticipateInPoll = nameof(CanParticipateInPoll);
    public const string CanUpdateAnswer = nameof(CanUpdateAnswer);

    public void RegisterPolicies(AuthorizationOptions options)
    {
        options.AddPolicy(CanCreatePoll, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Admin),
                nameof(UserRoles.Teacher)
            ).RequireNotBlocked();
        });

        options.AddPolicy(CanUpdatePoll, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Admin),
                nameof(UserRoles.Teacher)
            ).RequireNotBlocked();
        });

        options.AddPolicy(CanDeletePoll, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Admin),
                nameof(UserRoles.Teacher)
            ).RequireNotBlocked();
        });

        options.AddPolicy(CanGetPolls, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Admin),
                nameof(UserRoles.Teacher)
            ).RequireNotBlocked();
        });

        options.AddPolicy(CanGetPoll, policy =>
        {
            policy.RequireAuthenticatedUser().RequireNotBlocked();
        });

        options.AddPolicy(CanGetPollResults, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Admin),
                nameof(UserRoles.Teacher)
            ).RequireNotBlocked();
        });

        // TODO: Only teachers and admins can get poll results of any user, other roles can only get their own results
        options.AddPolicy(CanGetPollResult, policy =>
        {
            policy.RequireAuthenticatedUser().RequireNotBlocked();
        });

        // TODO: Add logic to check if the user can participate in the poll
        options.AddPolicy(CanParticipateInPoll, policy =>
        {
            policy.RequireAuthenticatedUser().RequireNotBlocked();
        });

        options.AddPolicy(CanUpdateAnswer, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Admin),
                nameof(UserRoles.Teacher)
            ).RequireNotBlocked();
        });
    }
}
