using Noo.Api.Core.DataAbstraction;

namespace Noo.Api.Polls.Types;

public enum PollQuestionType
{
    Checkbox,
    SingleChoice,
    MultipleChoice,
    Text,
    Number,
    Date,
    DateTime,
    Rating,
    Files,
}
