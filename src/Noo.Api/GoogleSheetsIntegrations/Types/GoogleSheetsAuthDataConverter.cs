using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Noo.Api.GoogleSheetsIntegrations.Types;

public class GoogleSheetsAuthDataConverter : ValueConverter<GoogleSheetsAuthData, string>
{
    public GoogleSheetsAuthDataConverter() : base(
        v => v.Serialize(),
        v => GoogleSheetsAuthData.Deserialize(v))
    {
    }
}
