using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Noo.Api.Core.DataAbstraction.Model;

public class UlidToBytesConverter : ValueConverter<Ulid, byte[]>
{
    public UlidToBytesConverter()
        : base(
            ulid => ulid.ToByteArray(),
            bytes => new Ulid(bytes)
        )
    { }
}
