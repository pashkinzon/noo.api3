using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Noo.Api.Polls.Types;

public class PollAnswerValueConverter : ValueConverter<PollAnswerValue, string>
{
    public PollAnswerValueConverter() : base(
        v => v.Serialize(),
        v => PollAnswerValue.Deserialize(v))
    {
    }
}
