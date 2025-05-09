using System.Text.Json;
using System.Text.Json.Serialization;

namespace Noo.Api.Core.Utils.Richtext.Delta;

public class DeltaInsertConverter : JsonConverter<object?>
{
    public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.String => reader.GetString(),
            JsonTokenType.StartObject => JsonSerializer.Deserialize<Dictionary<string, object>>(ref reader, options),
            _ => throw new JsonException("Invalid Delta insert value type")
        };
    }

    public override void Write(Utf8JsonWriter writer, object? value, JsonSerializerOptions options)
    {
        if (value is string str)
        {
            writer.WriteStringValue(str);
        }
        else if (value is IDictionary<string, object> dict)
        {
            JsonSerializer.Serialize(writer, dict, options);
        }
        else
        {
            throw new JsonException("Invalid insert value type");
        }
    }
}
