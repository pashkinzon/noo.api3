using Microsoft.AspNetCore.Authorization;
using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.Calendar;

public class CalendarPolicies : IPolicyRegistrar
{
    public const string CanGetCalendarEvents = nameof(CanGetCalendarEvents);

    public void RegisterPolicies(AuthorizationOptions options)
    {
        throw new NotImplementedException();
    }
}
