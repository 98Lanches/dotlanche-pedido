using DotLanches.Pedidos.Domain.Enums;

namespace DotLanches.Pedidos.PagamentoServiceClient.DTOs
{
    public class RegisterPagamentoForPedidoRequest
    {
        public Guid IdPedido { get; set; }

        public decimal Amount { get; set; }

        public TipoPagamento Type { get; set; }
    }
}
