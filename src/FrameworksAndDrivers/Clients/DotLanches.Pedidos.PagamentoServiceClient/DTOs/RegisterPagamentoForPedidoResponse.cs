namespace DotLanches.Pedidos.PagamentoServiceClient.DTOs
{
    internal class RegisterPagamentoForPedidoResponse
    {
        public bool OperationSuccessful { get; set; }

        public Guid RegistroPagamentoId { get; set; }

        public Guid PedidoId { get; set; }

        public bool IsAccepted { get; set; }

        public DateTime RegisteredTime { get; set; }

        public Dictionary<string, string>? ProviderData { get; set; }
    }
}