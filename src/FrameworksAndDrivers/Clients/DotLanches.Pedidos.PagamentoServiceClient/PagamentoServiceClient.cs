using DotLanches.Pedidos.Domain.Entities;
using DotLanches.Pedidos.Domain.Enums;
using DotLanches.Pedidos.Domain.Interfaces.Clients;
using DotLanches.Pedidos.Domain.ValueObjects;
using DotLanches.Pedidos.Gateways.Exceptions;
using DotLanches.Pedidos.PagamentoServiceClient.DTOs;
using System.Net.Http.Json;

namespace DotLanches.Pedidos.PagamentoServiceClient
{
    public class PagamentoServiceClient : IPagamentoServiceClient
    {
        private const string ServiceName = "PagamentosApi";
        private readonly HttpClient httpClient;

        public PagamentoServiceClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Pagamento> RegisterPagamentoForPedido(Pedido pedido)
        {
            var request = new RegisterPagamentoForPedidoRequest()
            {
                IdPedido = pedido.Id,
                Amount = pedido.TotalPrice,
                Type = pedido.Pagamento.Type,
            };

            const string route = "/api/pagamentos";

            var response = await httpClient.PostAsJsonAsync(route, request);

            if (!response.IsSuccessStatusCode)
                throw new ServiceIntegrationException(ServiceName, $"Could not connect to service. status code returned {response.StatusCode}");

            var responseData = await response.Content.ReadFromJsonAsync<RegisterPagamentoForPedidoResponse>();

            if (responseData is null || responseData.OperationSuccessful is false)
                throw new ServiceIntegrationException(ServiceName, "Could not register pagamento for pedido!");

            var pagamento = new Pagamento()
            {
                Accepted = responseData.IsAccepted,
                Amount = pedido.Pagamento.Amount,
                Type = pedido.Pagamento.Type,
            };

            if (pedido.Pagamento.Type == TipoPagamento.QrCode
                    && responseData.ProviderData!.TryGetValue("QR_CODE_IMG", out var qrCode))
                pagamento.PagamentoData = qrCode;

            return pagamento;
        }
    }
}