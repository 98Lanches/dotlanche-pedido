using DotLanches.Pedidos.Domain.Entities;
using DotLanches.Pedidos.Domain.ValueObjects;

namespace DotLanches.Pedidos.Domain.Interfaces.Gateways
{
    public interface IPagamentoGateway
    {
        Task<Pagamento> RequestPagamento(Pedido pedido);
    }
}
