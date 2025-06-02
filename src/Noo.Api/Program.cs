using Noo.Api.Core.Initialization.App;
using Noo.Api.Core.Initialization.ServiceCollection;
using Noo.Api.Core.Initialization.WebHostBuilder;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider((context, options) =>
{
    options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
    options.ValidateOnBuild = true;
});

builder.Services.LoadEnvConfigs(builder.Configuration);
builder.Services.AddNooDbContext(builder.Configuration);
builder.Services.AddNooAuthentication(builder.Configuration);
builder.Services.AddNooAuthorization();
builder.Services.AddNooSwagger(builder.Configuration);
builder.Services.AddLogger(builder.Configuration);
builder.Services.RegisterDependencies();
builder.Services.AddNooControllersAndConfigureJson();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCorsPolicy(builder.Configuration);
builder.Services.AddNooResponseCompression();
builder.Services.AddHealthcheckServices();
builder.Services.AddRequestRateLimiter();
// TODO: understand response caching and add it
// builder.Services.AddNooResponseCaching(builder.Configuration);
builder.Services.AddRouting();
builder.Services.AddAutoMapperProfiles();
builder.Services.AddCacheProvider(builder.Configuration);
// TODO: builder.Services.AddMetrics();
// TODO: builder.Services.AddHostFiltering();

builder.WebHost.AddWebServerConfiguration(builder.Configuration);

var app = builder.Build();

app.UseForwardedHeaders();
app.UseRouting();
app.UseRateLimiter();
app.UseNooSwagger(app.Configuration);
app.UseCors();
app.UseResponseCompression();
//app.UseResponseCaching();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHealthAllChecks();
app.UseExceptionHandling();

await app.RunAsync();
