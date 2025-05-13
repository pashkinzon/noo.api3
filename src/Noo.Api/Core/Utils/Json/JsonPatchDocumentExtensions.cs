using Microsoft.AspNetCore.Mvc.ModelBinding;
using SystemTextJsonPatch;

namespace Noo.Api.Core.Utils.Json;

public static class JsonPatchDocumentExtensions
{
    public static void ApplyToAndValidate<T>(
        this JsonPatchDocument<T> patch,
        T target,
        ModelStateDictionary? modelState = null
    ) where T : class
    {
        modelState ??= new ModelStateDictionary();

        patch.ApplyTo(target, (error) =>
            modelState.AddModelError(error.Operation.ToString() ?? "Unknown operation", error.ErrorMessage)
        );
    }
}
