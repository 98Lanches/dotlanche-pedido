namespace DotLanches.Pedidos.Presenters.Dtos
{
    public class RegisterPagamentoForPedidoRequest
    {
        public Guid PedidoId { get; set; }
        public Guid RegistroPagamentoId { get; set; }
    }
}