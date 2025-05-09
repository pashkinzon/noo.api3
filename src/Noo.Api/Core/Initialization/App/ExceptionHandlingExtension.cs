using Noo.Api.Core.Exceptions;

namespace Noo.Api.Core.Initialization.App;

public static class ExceptionHandlingExtension
{
    public static void UseExceptionHandling(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            return;
        }

        app.UseExceptionHandler(appError =>
        {
            var handler = app.Services.GetRequiredService<PipelineExceptionHandler>();
            appError.Run(async context => await handler.HandleExceptionAsync(context));
        });
    }
}
