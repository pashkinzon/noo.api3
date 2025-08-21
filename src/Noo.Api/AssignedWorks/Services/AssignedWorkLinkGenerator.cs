using System.Text.Json;
using Noo.Api.Core.Utils.DI;

namespace Noo.Api.AssignedWorks.Services;

[RegisterTransient(typeof(IAssignedWorkLinkGenerator))]
public class AssignedWorkLinkGenerator : IAssignedWorkLinkGenerator
{
    public string GenerateViewLink(Ulid assignedWorkId) => JsonSerializer.Serialize(new
    {
        name = "assigned-works.view",
        query = new
        {
            assignedWorkId = assignedWorkId.ToString()
        }
    });
}
