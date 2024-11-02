using DotLanches.Pedidos.Domain.Entities;

namespace DotLanches.Pedidos.Domain.Interfaces.Gateways
{
    public interface IPedidoGateway
    {
        Task Add(Pedido pedido);

        Task<Pedido> Update(Pedido pedido);

        Task<Pedido?> GetById(Guid id);
    }
}
