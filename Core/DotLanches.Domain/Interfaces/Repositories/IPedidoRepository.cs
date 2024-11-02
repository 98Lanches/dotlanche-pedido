using DotLanches.Pedido.Domain.Entities;

namespace DotLanches.Pedido.Domain.Interfaces.Repositories
{
    public interface IPedidoRepository
    {
        Task Add(Pedido pedido);

        Task<Pedido> Update(Pedido pedido);

        Task<Pedido?> GetById(Guid id);

        Task<Pedido> UpdateStatus(Pedido pedido);
    }
}
