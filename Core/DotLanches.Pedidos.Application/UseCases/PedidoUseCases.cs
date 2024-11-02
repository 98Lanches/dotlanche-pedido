#pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.

using DotLanches.Pedidos.Domain.Entities;
using DotLanches.Pedidos.Domain.Exceptions;
using DotLanches.Pedidos.Domain.Interfaces.Gateways;

namespace DotLanches.Pedidos.Application.UseCases
{
    public static class PedidoUseCases
    {
        public static async Task<Pedido> Create(Pedido pedido, IPedidoGateway pedidoGateway)
        {
            if (pedido == null)
                throw new ArgumentNullException(nameof(pedido));

            await pedidoGateway.Add(pedido);
            return pedido;
        }
    }
}
