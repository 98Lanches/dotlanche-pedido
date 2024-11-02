using DotLanches.Pedidos.Domain.Entities;
using DotLanches.Pedidos.Domain.Interfaces.Gateways;

namespace DotLanches.Pedidos.Application.UseCases
{
    public static class PedidoUseCases
    {
        public static async Task<Pedido> Create(Pedido pedido, IPedidoGateway pedidoGateway)
        {
            ArgumentNullException.ThrowIfNull(pedido);

            await pedidoGateway.Add(pedido);
            return pedido;
        }
    }
}
