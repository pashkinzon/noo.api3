
using System.Text.Json;

namespace Noo.Api.Polls.Types;

public struct PollUserExternalData
{
    public static PollUserExternalData Deserialize(string v)
    {
        return JsonSerializer.Deserialize<PollUserExternalData>(v);
    }

    public string Serialize()
    {
        return JsonSerializer.Serialize(this);
    }
}
