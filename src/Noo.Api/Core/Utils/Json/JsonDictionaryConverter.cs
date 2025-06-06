using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Noo.Api.Core.Utils.Json;

public class JsonDictionaryConverter<TValue> : ValueConverter<Dictionary<string, TValue>, string>
    where TValue : notnull
{
    public JsonDictionaryConverter()
        : base(
            dict => JsonSerializer.Serialize(dict, JsonSerializerOptions.Default),
            json => JsonSerializer.Deserialize<Dictionary<string, TValue>>(
                json, JsonSerializerOptions.Default
            ) ?? new Dictionary<string, TValue>()
        )
    {
    }
}
