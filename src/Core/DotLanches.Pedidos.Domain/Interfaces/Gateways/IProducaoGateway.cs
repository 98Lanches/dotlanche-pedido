using DotLanches.Pedidos.Domain.Entities;

namespace DotLanches.Pedidos.Domain.Interfaces.Gateways
{
    public interface IProducaoGateway
    {
        Task<bool> StartProducaoPedido(Pedido pedido);
    }
}
