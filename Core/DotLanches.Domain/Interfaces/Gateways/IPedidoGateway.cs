using DotLanches.Domain.Entities;

namespace DotLanches.Domain.Interfaces.Gateways
{
    public interface IPedidoGateway
    {
        Task Add(Pedido pedido);

        Task<Pedido> Update(Pedido pedido);

        Task<int> GetLastQueueKeyNumber();

        Task<IEnumerable<Pedido>> GetPedidosQueue();

        Task<Pedido?> GetById(Guid id);

        Task<Pedido> UpdateStatus(Pedido pedido);
    }
}
