using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Platform.DTO;

namespace Noo.Api.Platform.Services;

public interface IPlatformService
{
    public string GetPlatformVersion();

    public SearchResult<ChangeLogDTO> GetChangelog();
}
