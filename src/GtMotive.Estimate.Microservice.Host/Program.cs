using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using GtMotive.Estimate.Microservice.Api;
using GtMotive.Estimate.Microservice.ApplicationCore.Mappings;
using GtMotive.Estimate.Microservice.Host.Configuration;
using GtMotive.Estimate.Microservice.Host.DependencyInjection;
using GtMotive.Estimate.Microservice.Infrastructure;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings;
using IdentityServer4.AccessTokenValidation;
using MediatR;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder();

builder.Logging.ClearProviders(); // Elimina los proveedores predeterminados
builder.Logging.AddConsole(options =>
{
    options.LogToStandardErrorThreshold = LogLevel.Information;
});
builder.Logging.SetMinimumLevel(LogLevel.Trace); // Establece el nivel mínimo de log
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();
builder.Host.UseSerilog();

// Servicios.
if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddApplicationInsightsTelemetry(builder.Configuration);
    builder.Services.AddApplicationInsightsKubernetesEnricher();
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(VehicleProfile).Assembly);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
var appSettings = appSettingsSection.Get<AppSettings>();
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDb"));

builder.Services.AddControllers(ApiConfiguration.ConfigureControllers)
    .WithApiControllers();

var assemblies = new[]
{
    Assembly.Load("GtMotive.Estimate.Microservice.ApplicationCore"),
    Assembly.Load("GtMotive.Estimate.Microservice.Domain"),
    Assembly.Load("GtMotive.Estimate.Microservice.Api"),
    Assembly.Load("GtMotive.Estimate.Microservice.Infrastructure"),
    Assembly.Load("GtMotive.Estimate.Microservice.Host")
};

// Registrar MongoService
builder.Services.AddSingleton<MongoService>();

// Registrar servicios
foreach (var assembly in assemblies)
{
    var serviceTypes = assembly.GetTypes()
        .Where(t => t.Name.EndsWith("Service", StringComparison.OrdinalIgnoreCase) && t.IsClass);

    foreach (var service in serviceTypes)
    {
        var interfaceType = service.GetInterfaces().FirstOrDefault(i => i.Name == "I" + service.Name);
        if (interfaceType != null)
        {
            builder.Services.AddTransient(interfaceType, service);
        }
    }
}

// Registrar repositorios
foreach (var assembly in assemblies)
{
    var repositoryTypes = assembly.GetTypes()
        .Where(t => t.Name.EndsWith("Repository", StringComparison.OrdinalIgnoreCase) && t.IsClass);

    foreach (var repository in repositoryTypes)
    {
        var interfaceType = repository.GetInterfaces().FirstOrDefault(i => i.Name == "I" + repository.Name);
        if (interfaceType != null)
        {
            builder.Services.AddTransient(interfaceType, repository);
        }
    }
}

// Registrar manejadores de MediatR
builder.Services.AddMediatR(assemblies);

foreach (var assembly in assemblies)
{
    var handlerTypes = assembly.GetTypes()
        .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)));

    foreach (var handler in handlerTypes)
    {
        var interfaceTypes = handler.GetInterfaces()
            .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));

        foreach (var interfaceType in interfaceTypes)
        {
            builder.Services.AddTransient(interfaceType, handler);
        }
    }
}

builder.Services.AddBaseInfrastructure(builder.Environment.IsDevelopment());

// Configuración de autenticación JWT.
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                               ForwardedHeaders.XForwardedProto;

    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
})
.AddIdentityServerAuthentication(options =>
{
    options.Authority = appSettings.JwtAuthority;
    options.ApiName = "estimate-api";
    options.SupportedTokens = SupportedTokens.Jwt;
});

// Configuración de Swagger.
builder.Services.AddSwagger(appSettings, builder.Configuration);

// Construcción de la aplicación.
var app = builder.Build();

// Configuración de logging.
Log.Logger = builder.Environment.IsDevelopment() ?
    new LoggerConfiguration()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
        .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
        .MinimumLevel.Override("System", LogEventLevel.Warning)
        .WriteTo.Console(
            outputTemplate:
            "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
            theme: AnsiConsoleTheme.Literate)
        .CreateLogger() :
    new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Application", "addoperation")
        .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .WriteTo.ApplicationInsights(
            app.Services.GetRequiredService<TelemetryConfiguration>(), TelemetryConverter.Traces)
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

var pathBase = new PathBase(builder.Configuration.GetValue("PathBase", defaultValue: PathBase.DefaultPathBase));

if (pathBase.IsDefault == false)
{
    app.UsePathBase(pathBase.CurrentWithoutTrailingSlash);
}

app.UseForwardedHeaders();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwaggerInApplication(pathBase, builder.Configuration);
app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
