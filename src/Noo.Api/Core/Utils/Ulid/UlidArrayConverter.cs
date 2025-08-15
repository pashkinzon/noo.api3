using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UlidType = System.Ulid;

namespace Noo.Api.Core.Utils.Ulid;

public class UlidArrayToJsonConverter : ValueConverter<UlidType[], string>
{
    public UlidArrayToJsonConverter() : base(
        v => JsonSerializer.Serialize(v, new JsonSerializerOptions { WriteIndented = false }),
        v => JsonSerializer.Deserialize<UlidType[]>(v, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? Array.Empty<UlidType>())
    {
    }
}
