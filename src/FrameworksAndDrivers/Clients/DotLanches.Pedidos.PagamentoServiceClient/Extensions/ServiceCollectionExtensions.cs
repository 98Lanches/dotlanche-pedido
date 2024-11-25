using DotLanches.Pedidos.Domain.Interfaces.Clients;
using DotLanches.Pedidos.Gateways.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotLanches.Pedidos.Integrations.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPagamentoServiceIntegration(this IServiceCollection services, IConfiguration config)
        {
            services.AddHttpClient<IPagamentoServiceClient, PagamentoServiceClient>(client =>
            {
                client.BaseAddress = new Uri(config["Integrations:PagamentoService:BaseAddress"] ??
                    throw new MisconfigurationException("Integrations:PagamentoService:BaseAddress"));
            });

            return services;
        }

        public static IServiceCollection AddProducaoServiceIntegration(this IServiceCollection services, IConfiguration config)
        {
            services.AddHttpClient<IProducaoServiceClient, ProducaoServiceClient>(client =>
            {
                client.BaseAddress = new Uri(config["Integrations:ProducaoService:BaseAddress"] ??
                    throw new MisconfigurationException("Integrations:ProducaoService:BaseAddress"));
            });

            return services;
        }
    }
}