namespace Noo.Api.Polls.Types;

public struct PollAnswerValue
{
    public PollQuestionType Type { get; set; }
    public object? Value { get; set; }
}
