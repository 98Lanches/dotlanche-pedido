#pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.

using DotLanches.Pedido.Domain.Entities;
using DotLanches.Pedido.Domain.Interfaces.Gateways;

namespace DotLanches.Pedido.Application.UseCases
{
    public static class PedidoUseCases
    {
        public static async Task<Pedido> Create(Pedido pedido, IPedidoGateway pedidoGateway)
        {
            //!TODO Add service to get recover price from produto

            pedido.CalculateTotalPrice();

            await pedidoGateway.Add(pedido);
            return pedido;
        }
    }
}