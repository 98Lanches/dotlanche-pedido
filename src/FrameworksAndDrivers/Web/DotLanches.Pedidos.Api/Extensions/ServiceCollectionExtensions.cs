using DotLanches.Pedidos.Api.Filters;
using DotLanches.Pedidos.DataMongo.Extensions;
using DotLanches.Pedidos.Integrations.Extensions;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using System.Reflection;

namespace DotLanches.Pedidos.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.ConfigureSwagger();

            services.ConfigureDatabase(configuration);
            services.AddPagamentoServiceIntegration(configuration);
            services.AddProducaoServiceIntegration(configuration);

            services.AddExceptionHandler<ExceptionFilter>();
            services.AddProblemDetails();

            services.ConfigureHealthChecks(configuration);

            return services;
        }

        private static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "DotLanches Pedidos API",
                    Description = "Serviço de criação de pedidos dotlanche"
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            return services;
        }

        private static IServiceCollection ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddMongoDb(configuration.GetConnectionString("DefaultConnection")
                                ?? throw new Exception("No connection string for mongodb provided!"),
                            timeout: TimeSpan.FromSeconds(60));

            return services;
        }

        public static IServiceCollection ConfigureLogging(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSerilog();

            return services;
        }

        public static IServiceCollection ConfigureOpenTelemetry(this IServiceCollection services, IConfiguration configuration)
        {
            var tracingOtlpEndpoint = configuration["OpenTelemetry:ProviderEndpointUrl"];
            var otel = services.AddOpenTelemetry();

            otel.ConfigureResource(resource => resource.AddService(serviceName: "Pedidos Api"));

            otel.WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddMeter("Microsoft.AspNetCore.Hosting")
                .AddMeter("Microsoft.AspNetCore.Server.Kestrel"));

            otel.WithTracing(tracing =>
            {
                tracing.AddAspNetCoreInstrumentation();
                tracing.AddHttpClientInstrumentation();
                if (tracingOtlpEndpoint != null)
                {
                    tracing.AddOtlpExporter(otlpOptions =>
                     {
                         otlpOptions.Endpoint = new Uri(tracingOtlpEndpoint);
                     });
                }
            });

            return services;
        }
    }
}