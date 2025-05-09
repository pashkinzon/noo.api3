using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.Core.Initialization;

public abstract class ApplicationModule
{
    public virtual IPolicyRegistrar[] PolicyRegistrars { get; } = [];

    public virtual void RegisterServices(IServiceCollection services) { }

    public virtual void RegisterRepositories(IServiceCollection services) { }
}
