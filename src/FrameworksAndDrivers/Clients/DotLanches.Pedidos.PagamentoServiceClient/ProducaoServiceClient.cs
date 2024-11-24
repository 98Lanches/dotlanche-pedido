using DotLanches.Pedidos.Domain.Entities;
using DotLanches.Pedidos.Domain.Interfaces.Clients;
using DotLanches.Pedidos.Gateways.Exceptions;
using DotLanches.Pedidos.Integrations.DTOs;
using System.Net.Http.Json;

namespace DotLanches.Pedidos.Integrations
{
    public class ProducaoServiceClient : IProducaoServiceClient
    {
        private const string ServiceName = "ProducaoApi";
        private readonly HttpClient httpClient;

        public ProducaoServiceClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> StartProducaoPedido(Pedido pedido)
        {
            var request = new StartProducaoPedidoRequest()
            {
                PedidoId = pedido.Id,
                Combos = pedido.Combos.Select(x =>
                    new ComboDto()
                    {
                        Id = pedido.Id,
                        ProdutoIds = x.IdsProduto
                    }
                )
            };

            const string route = "/api/producao";

            var response = await httpClient.PostAsJsonAsync(route, request);

            if (!response.IsSuccessStatusCode)
                throw new ServiceIntegrationException(ServiceName, $"Could not connect to service. status code returned {response.StatusCode}");

            var responseData = await response.Content.ReadFromJsonAsync<StartProducaoPedidoResponse>();

            return responseData?.Success ?? false;
        }
    }
}