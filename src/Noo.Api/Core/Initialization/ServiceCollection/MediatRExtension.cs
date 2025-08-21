namespace Noo.Api.Core.Initialization.ServiceCollection;

public static class MediatRExtension
{
    public static void AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Noo.Api.AssignedWorks.Services.AssignedWorkService).Assembly));
    }
}
