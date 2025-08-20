namespace Noo.Api.Sessions;

public static class SessionConfig
{
    // TTLs and thresholds
    public const int OnlineTtlMinutes = 15;
    public const int ActiveTtlDays = 14;

    // Middleware DB update throttle window
    public const int DbUpdateThrottleMinutes = 5;

    // Background cleanup worker cadence and retention
    public const int CleanupIntervalHours = 12;
    public const int SessionRetentionDays = 30;

    // Derived TimeSpans for convenience
    public static readonly TimeSpan OnlineTtl = TimeSpan.FromMinutes(OnlineTtlMinutes);
    public static readonly TimeSpan ActiveTtl = TimeSpan.FromDays(ActiveTtlDays);
    public static readonly TimeSpan DbUpdateThrottle = TimeSpan.FromMinutes(DbUpdateThrottleMinutes);
    public static readonly TimeSpan CleanupInterval = TimeSpan.FromHours(CleanupIntervalHours);
}
