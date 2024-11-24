namespace DotLanches.Pedidos.Integrations.DTOs
{
    internal class StartProducaoPedidoResponse
    {
        public required bool Success { get; set; }
        public required Guid PedidoId { get; set; }
    }
}
