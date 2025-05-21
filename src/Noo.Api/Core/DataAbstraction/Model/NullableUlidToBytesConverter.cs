using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Noo.Api.Core.DataAbstraction.Model;

public class NullableUlidToBytesConverter : ValueConverter<Ulid?, byte[]?>
{
    public NullableUlidToBytesConverter()
        : base(
            ulid => ulid == null ? null : ulid.Value.ToByteArray(),
            bytes => bytes == null ? null : new Ulid(bytes)
        )
    { }
}
