using Asp.Versioning.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Payments.Api.Extensions.Auth;
using Payments.Api.Extensions.Auth.Middleware;
using Payments.Api.Extensions.Logs;
using Payments.Api.Extensions.Logs.ELK;
using Payments.Api.Extensions.Logs.Extension;
using Payments.Api.Extensions.Mappers;
using Payments.Api.Extensions.Migration;
using Payments.Api.Extensions.Swagger;
using Payments.Api.Extensions.Swagger.Middleware;
using Payments.Api.Extensions.Tracing;
using Payments.Api.Extensions.Versioning;
using Payments.Application;
using Payments.Infrastructure;
using Payments.Infrastructure.DataBase.EntityFramework.Context;
using Payments.Infrastructure.Monitoring;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilogConfiguration();
builder.WebHost.UseUrls("http://*:80");

builder.Services.AddMvcCore(options => options.AddLogRequestFilter());
builder.Services.AddVersioning();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthorizationExtension(builder.Configuration);

// Adiciona configuração CORS para permitir solicitações do Prometheus
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Adiciona monitoramento com Prometheus
builder.Services.AddPrometheusMonitoring();

// Adiciona integração ELK
builder.Services.AddELKIntegration(builder.Configuration);

// Distributed Tracing with OpenTelemetry + Jaeger
builder.Services.AddDistributedTracing(builder.Configuration);

#region [DI]

ApplicationBootstrapper.Register(builder.Services);
InfraBootstrapper.Register(builder.Services);

#endregion

var app = builder.Build();

app.ExecuteMigrations();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseAuthentication();                        // 1°: popula HttpContext.User
app.UseMiddleware<RoleAuthorizationMiddleware>(); // 2°: seu middleware
app.UseCorrelationId();
app.UseELKIntegration();

// Adiciona CORS antes de outros middlewares
app.UseCors("AllowAll");

// Adiciona middleware de monitoramento
app.UsePrometheusMonitoring();

// Adiciona request logging com Serilog
app.UseSerilogRequestLogging();

app.UseVersionedSwagger(apiVersionDescriptionProvider);
app.UseAuthorization();                         // 3°: aplica [Authorize]
app.MapControllers();
app.Run();