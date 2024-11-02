using DotLanches.Pedido.Api.Filters;
using DotLanches.Pedido.DataMongo.Extensions;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace DotLanches.Pedido.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.ConfigureSwagger();

            services.ConfigureDatabase(configuration);
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
                    Title = "DotLanches API",
                    Description = "Backend de gerenciamento LanchesJaamp"
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
    }
}