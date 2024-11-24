namespace DotLanches.Pedidos.Presenters.Dtos
{
    public class AcceptPagamentoRequest
    {
        public Guid PedidoId { get; set; }
        public Guid RegistroPagamentoId { get; set; }
    }
}