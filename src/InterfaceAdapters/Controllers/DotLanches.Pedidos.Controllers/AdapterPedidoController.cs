using DotLanches.Pedidos.Application.UseCases;
using DotLanches.Pedidos.Domain.Entities;
using DotLanches.Pedidos.Domain.Interfaces.Repositories;
using DotLanches.Pedidos.Gateways;

namespace DotLanches.Pedidos.Controllers
{
    public class AdapterPedidoController
    {
        private readonly IPedidoRepository _pedidoRepository;

        public AdapterPedidoController(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<Guid> Create(Pedido pedido)
        {
            var pedidoGateway = new PedidoGateway(_pedidoRepository);
            var newPedido = await PedidoUseCases.Create(pedido, pedidoGateway);

            return newPedido.Id;
        }
    }
}
