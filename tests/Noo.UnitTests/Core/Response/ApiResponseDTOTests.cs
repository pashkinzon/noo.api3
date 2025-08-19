using System.Text.Json;
using Noo.Api.Core.Response;

namespace Noo.UnitTests.Core.Response;

public class ApiResponseDTOTests
{
    [Fact]
    public void ApiResponseDTO_Serializes_WithDataAndOptionalMeta()
    {
        var dto = new ApiResponseDTO<string>("hello", new { page = 1 });
        var json = JsonSerializer.Serialize(dto);
        Assert.Contains("\"data\":\"hello\"", json);
        Assert.Contains("\"meta\":{\"page\":1}", json);
    }

    [Fact]
    public void ApiResponseDTO_OmitsNullMeta()
    {
        var dto = new ApiResponseDTO<string>("hello", null);
        var json = JsonSerializer.Serialize(dto);
        Assert.Contains("\"data\":\"hello\"", json);
        Assert.DoesNotContain("\"meta\"", json);
    }

    [Fact]
    public void ErrorApiResponseDTO_Serializes()
    {
        var dto = new ErrorApiResponseDTO(new { code = "X" });
        var json = JsonSerializer.Serialize(dto);
        Assert.Contains("\"error\":{\"code\":\"X\"}", json);
    }

    [Fact]
    public void IdResponseDTO_Serializes()
    {
        var id = Ulid.NewUlid();
        var dto = new IdResponseDTO(id);
        var json = JsonSerializer.Serialize(dto);
        Assert.Contains("\"id\":\"" + id.ToString() + "\"", json);
    }
}
