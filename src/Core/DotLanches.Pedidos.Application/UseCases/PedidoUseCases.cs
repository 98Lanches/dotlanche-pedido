using DotLanches.Pedidos.Domain.Entities;
using DotLanches.Pedidos.Domain.Interfaces.Gateways;
using DotLanches.Pedidos.UseCases.Exceptions;

namespace DotLanches.Pedidos.Application.UseCases
{
    public static class PedidoUseCases
    {
        public static async Task<Pedido> Create(Pedido pedido, IPedidoGateway pedidoGateway, IPagamentoGateway pagamentoGateway)
        {
            ArgumentNullException.ThrowIfNull(pedido);

            var pagamentoData = await pagamentoGateway.RequestPagamento(pedido);
            pedido.Pagamento = pagamentoData;

            await pedidoGateway.Add(pedido);

            return pedido;
        }

        public static async Task<Pedido?> GetById(Guid idPedido, IPedidoGateway pedidoGateway)
        {
            return await pedidoGateway.GetById(idPedido);
        }

        public static async Task RegisterPagamentoForPedido(Guid idPedido, Guid idRegistroPagamento, IPedidoGateway pedidoGateway)
        {
            var pedido = await pedidoGateway.GetById(idPedido) ??
                throw new UseCaseException($"pedido {idPedido} was not found!");

            pedido.Pagamento.Accepted = true;
            pedido.Pagamento.Id = idRegistroPagamento;

            await pedidoGateway.Update(pedido);

            // TO DO: Start Producao Pedido after update, calling producao service
        }

        public static async Task StartProducaoPedido(Pedido pedido)
        {
            throw new NotImplementedException();
        }
    }
}