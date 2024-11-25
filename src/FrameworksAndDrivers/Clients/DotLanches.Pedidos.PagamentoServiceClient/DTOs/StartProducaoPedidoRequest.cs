namespace DotLanches.Pedidos.Integrations.DTOs
{
    internal class StartProducaoPedidoRequest
    {
        public required Guid PedidoId { get; set; }
        public required IEnumerable<ComboDto> Combos { get; set; }
    }

    internal record ComboDto
    {
        public required Guid Id { get; set; }

        public required IEnumerable<Guid> ProdutoIds { get; set; }
    }
}