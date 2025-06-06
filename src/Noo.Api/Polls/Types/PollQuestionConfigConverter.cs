using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Noo.Api.Polls.Types;

public class PollQuestionConfigConverter : ValueConverter<PollQuestionConfig, string>
{
    public PollQuestionConfigConverter() : base(
        v => v.Serialize(),
        v => PollQuestionConfig.Deserialize(v))
    {
    }
}
