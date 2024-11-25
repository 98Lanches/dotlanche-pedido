using DotLanches.Pedidos.Domain.Entities;
using DotLanches.Pedidos.Domain.ValueObjects;

namespace DotLanches.Pedidos.Domain.Interfaces.Clients
{
    public interface IPagamentoServiceClient
    {
        Task<Pagamento> RegisterPagamentoForPedido(Pedido pedido);
    }
}
