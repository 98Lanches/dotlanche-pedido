using DotLanches.Pedidos.Domain.Entities;

namespace DotLanches.Pedidos.Domain.Interfaces.Clients
{
    public interface IProducaoServiceClient
    {
        Task<bool> StartProducaoPedido(Pedido pedido);
    }
}
