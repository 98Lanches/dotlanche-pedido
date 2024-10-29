using DotLanches.Domain.Entities;

namespace DotLanches.Domain.Interfaces.Repositories
{
    public interface IPedidoRepository
    {
        Task Add(Pedido pedido);

        Task<Pedido> Update(Pedido pedido);
        Task<IEnumerable<Pedido>> GetPedidosQueue();

        Task<Pedido?> GetById(Guid id);

        Task<Pedido> UpdateStatus(Pedido pedido);
    }
}
