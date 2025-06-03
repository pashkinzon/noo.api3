using Microsoft.AspNetCore.Authorization;
using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.Calendar;

public class CalendarPolicies : IPolicyRegistrar
{
    public const string CanGetCalendarEvents = nameof(CanGetCalendarEvents);
    public const string CanCreateCalendarEvent = nameof(CanCreateCalendarEvent);

    public void RegisterPolicies(AuthorizationOptions options)
    {
        options.AddPolicy(CanGetCalendarEvents, policy =>
        {
            policy.RequireAuthenticatedUser().RequireNotBlocked();
        });

        options.AddPolicy(CanCreateCalendarEvent, policy =>
        {
            policy.RequireAuthenticatedUser().RequireNotBlocked();
        });
    }
}
