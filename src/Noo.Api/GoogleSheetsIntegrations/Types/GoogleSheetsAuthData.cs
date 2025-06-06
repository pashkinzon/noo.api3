
using System.Text.Json;

namespace Noo.Api.GoogleSheetsIntegrations.Types;

// TODO: implement
public struct GoogleSheetsAuthData
{
    public static GoogleSheetsAuthData Deserialize(string v)
    {
        return JsonSerializer.Deserialize<GoogleSheetsAuthData>(v);
    }

    public string Serialize()
    {
        return JsonSerializer.Serialize(this);
    }
}
