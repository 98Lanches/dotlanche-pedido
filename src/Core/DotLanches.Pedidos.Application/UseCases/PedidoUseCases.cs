using DotLanches.Pedidos.Domain.Entities;
using DotLanches.Pedidos.Domain.Interfaces.Gateways;
using DotLanches.Pedidos.UseCases.Exceptions;

namespace DotLanches.Pedidos.Application.UseCases
{
    public static class PedidoUseCases
    {
        public static async Task<Pedido> Create(Pedido pedido, IDatabaseGateway databaseGateway, IPagamentoGateway pagamentoGateway)
        {
            ArgumentNullException.ThrowIfNull(pedido);

            var pagamentoData = await pagamentoGateway.RequestPagamento(pedido);
            pedido.Pagamento = pagamentoData;

            await databaseGateway.Add(pedido);

            return pedido;
        }

        public static async Task<Pedido?> GetById(Guid idPedido, IDatabaseGateway pedidoGateway)
        {
            return await pedidoGateway.GetById(idPedido);
        }

        public static async Task RegisterPagamentoForPedido(Guid idPedido, Guid idRegistroPagamento, IDatabaseGateway databaseGateway, IProducaoGateway producaoGateway)
        {
            var pedido = await databaseGateway.GetById(idPedido) ??
                throw new UseCaseException($"pedido {idPedido} was not found!");

            pedido.Pagamento.Accepted = true;
            pedido.Pagamento.Id = idRegistroPagamento;

            await databaseGateway.Update(pedido);

            await producaoGateway.StartProducaoPedido(pedido);
        }
    }
}