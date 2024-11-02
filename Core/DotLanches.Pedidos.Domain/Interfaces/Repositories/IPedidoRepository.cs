using DotLanches.Pedidos.Domain.Entities;

namespace DotLanches.Pedidos.Domain.Interfaces.Repositories
{
    public interface IPedidoRepository
    {
        Task Add(Pedido pedido);

        Task<Pedido> Update(Pedido pedido);

        Task<Pedido?> GetById(Guid id);
    }
}
