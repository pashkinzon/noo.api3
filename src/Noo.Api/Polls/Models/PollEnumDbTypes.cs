namespace Noo.Api.Polls.Models;

public static class PollEnumDbDataTypes
{
    public const string PollQuestionType = "ENUM('Checkbox', 'SingleChoice', 'MultipleChoice', 'Text', 'Number', 'Date', 'DateTime', 'Rating', 'Files')";

    public const string ParticipatingUserType = "ENUM('AuthenticatedUser', 'TelegramUser')";
}
