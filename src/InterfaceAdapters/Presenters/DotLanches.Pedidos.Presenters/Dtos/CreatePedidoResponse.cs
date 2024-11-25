namespace DotLanches.Pedidos.Presenters.Dtos
{
    public class CreatePedidoResponse
    {
        public Guid PedidoId { get; set; }

        public string? PagamentoInformation { get; set; }
    }
}
