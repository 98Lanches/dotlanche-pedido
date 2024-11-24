using DotLanches.Pedidos.Domain.Enums;

namespace DotLanches.Pedidos.Domain.ValueObjects
{
    public class Pagamento
    {
        public Guid? Id { get; set; }
        public bool Accepted { get; set; }
        public decimal Amount { get; set; }
        public TipoPagamento Type { get; set; }
        public string? PagamentoData { get; set; }
    }
}