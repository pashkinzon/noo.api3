namespace Noo.Api.Notifications;

public static class NotificationConfig
{
    public static TimeSpan KeepUnreadTime { get; } = TimeSpan.FromDays(7);

    public static TimeSpan KeepReadTime { get; } = TimeSpan.FromDays(5);

    public static string CheckForDeletionCronPattern { get; } = ""; // TODO: add pattern
}
