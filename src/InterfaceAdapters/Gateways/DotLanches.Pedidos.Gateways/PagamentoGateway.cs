using DotLanches.Pedidos.Domain.Entities;
using DotLanches.Pedidos.Domain.Interfaces.Clients;
using DotLanches.Pedidos.Domain.Interfaces.Gateways;
using DotLanches.Pedidos.Domain.ValueObjects;

namespace DotLanches.Pedidos.Gateways
{
    public class PagamentoGateway : IPagamentoGateway
    {
        private readonly IPagamentoServiceClient pagamentoServiceClient;

        public PagamentoGateway(IPagamentoServiceClient pagamentoServiceClient)
        {
            this.pagamentoServiceClient = pagamentoServiceClient;
        }

        public async Task<Pagamento> RequestPagamento(Pedido pedido)
        {
            return await pagamentoServiceClient.RegisterPagamentoForPedido(pedido);
        }
    }
}
