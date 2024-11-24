namespace DotLanches.Pedidos.Presenters.Dtos
{
    public class NotifyPagamentoPedidoRequest
    {
        public Guid PedidoId { get; set; }
        public Guid RegistroPagamentoId { get; set; }
    }
}