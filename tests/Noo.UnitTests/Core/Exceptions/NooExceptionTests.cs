using System.Net;
using Noo.Api.Core.Exceptions;

namespace Noo.UnitTests.Core.Exceptions;

public class NooExceptionTests
{
    [Fact]
    public void Serialize_ProducesExpectedShape()
    {
        var ex = new NooException("Boom")
        {
            Id = "CUSTOM",
            StatusCode = HttpStatusCode.BadRequest,
            Payload = new { a = 1 }
        };

        var obj = ex.Serialize();
        var id = obj.GetType().GetProperty("id")!.GetValue(obj);
        var statusCode = obj.GetType().GetProperty("statusCode")!.GetValue(obj);
        var message = obj.GetType().GetProperty("message")!.GetValue(obj);
        var payload = obj.GetType().GetProperty("payload")!.GetValue(obj);

        Assert.Equal("CUSTOM", id);
        Assert.Equal((int)HttpStatusCode.BadRequest, statusCode);
        Assert.Equal("Boom", message);
        Assert.NotNull(payload);
    }

    [Fact]
    public void SerializePublicly_HidesMessage_WhenInternal()
    {
        var ex = new NooException("Sensitive")
        {
            Id = "INTERNAL",
            StatusCode = HttpStatusCode.InternalServerError,
            IsInternal = true
        };

        var dto = ex.SerializePublicly();
        Assert.Equal("INTERNAL", dto.Id);
        Assert.Equal(500, dto.StatusCode);
        Assert.Equal("An error occurred. Please try again later.", dto.Message);
    }

    [Fact]
    public void FromUnhandled_SetsInternalAndLogId()
    {
        var unhandled = new InvalidOperationException("oops");
        var ex = NooException.FromUnhandled(unhandled);
        Assert.True(ex.IsInternal);
        Assert.False(string.IsNullOrWhiteSpace(ex.LogId));
        Assert.Equal(HttpStatusCode.InternalServerError, ex.StatusCode);
    }
}
