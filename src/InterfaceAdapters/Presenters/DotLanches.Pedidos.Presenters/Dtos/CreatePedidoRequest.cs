using DotLanches.Pedidos.Domain.Enums;

namespace DotLanches.Pedidos.Presenters.Dtos
{
    public class CreatePedidoRequest
    {
        public string? ClienteCpf { get; set; }

        public required TipoPagamento TipoPagamento { get; set; }

        public required IEnumerable<ComboDto> Combos { get; set; }
    }
}