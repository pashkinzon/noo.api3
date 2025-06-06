using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Noo.Api.Polls.Types;

public class PollUserExternalDataConverter : ValueConverter<PollUserExternalData, string>
{
    public PollUserExternalDataConverter() : base(
        v => v.Serialize(),
        v => PollUserExternalData.Deserialize(v))
    {
    }
}
