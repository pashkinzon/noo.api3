using System.Text.Json;
using Noo.Api.Core.Response;

namespace Noo.UnitTests.Core.Response;

public class ApiResponseDTOTests
{
    [Fact(DisplayName = "ApiResponseDTO serializes data with optional meta")]
    public void ApiResponseDTO_Serializes_WithDataAndOptionalMeta()
    {
        var dto = new ApiResponseDTO<string>("hello", new { page = 1 });
        var json = JsonSerializer.Serialize(dto);
        Assert.Contains("\"data\":\"hello\"", json);
        Assert.Contains("\"meta\":{\"page\":1}", json);
    }

    [Fact(DisplayName = "ApiResponseDTO omits meta when null")]
    public void ApiResponseDTO_OmitsNullMeta()
    {
        var dto = new ApiResponseDTO<string>("hello", null);
        var json = JsonSerializer.Serialize(dto);
        Assert.Contains("\"data\":\"hello\"", json);
        Assert.DoesNotContain("\"meta\"", json);
    }

    [Fact(DisplayName = "ErrorApiResponseDTO serializes error object")]
    public void ErrorApiResponseDTO_Serializes()
    {
        var dto = new ErrorApiResponseDTO(new { code = "X" });
        var json = JsonSerializer.Serialize(dto);
        Assert.Contains("\"error\":{\"code\":\"X\"}", json);
    }

    [Fact(DisplayName = "IdResponseDTO serializes Ulid as string")]
    public void IdResponseDTO_Serializes()
    {
        var id = Ulid.NewUlid();
        var dto = new IdResponseDTO(id);
        var json = JsonSerializer.Serialize(dto);
        Assert.Contains("\"id\":\"" + id.ToString() + "\"", json);
    }
}
