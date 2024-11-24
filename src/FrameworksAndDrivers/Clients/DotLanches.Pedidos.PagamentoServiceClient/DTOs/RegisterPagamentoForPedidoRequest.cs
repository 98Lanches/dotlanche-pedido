using DotLanches.Pedidos.Domain.Enums;

namespace DotLanches.Pedidos.Integrations.DTOs
{
    public class RegisterPagamentoForPedidoRequest
    {
        public Guid IdPedido { get; set; }

        public decimal Amount { get; set; }

        public TipoPagamento Type { get; set; }
    }
}
