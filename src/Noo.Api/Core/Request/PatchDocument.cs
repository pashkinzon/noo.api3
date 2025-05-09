using SystemTextJsonPatch;

namespace Noo.Api.Core.Request;

public class PatchDocument<T> : JsonPatchDocument<T> where T : class
{
}
