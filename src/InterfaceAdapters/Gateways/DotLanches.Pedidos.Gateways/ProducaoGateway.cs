using DotLanches.Pedidos.Domain.Entities;
using DotLanches.Pedidos.Domain.Interfaces.Clients;
using DotLanches.Pedidos.Domain.Interfaces.Gateways;

namespace DotLanches.Pedidos.Gateways
{
    public class ProducaoGateway : IProducaoGateway
    {
        private readonly IProducaoServiceClient producaoServiceClient;

        public ProducaoGateway(IProducaoServiceClient producaoServiceClient)
        {
            this.producaoServiceClient = producaoServiceClient;
        }

        public async Task<bool> StartProducaoPedido(Pedido pedido)
        {
            return await producaoServiceClient.StartProducaoPedido(pedido);
        }
    }
}
