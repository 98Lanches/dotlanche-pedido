using DotLanches.Pedido.Application.UseCases;
using DotLanches.Pedido.Domain.Entities;
using DotLanches.Pedido.Domain.Interfaces.Repositories;
using DotLanches.Pedido.Gateways;

namespace DotLanches.Pedido.Controllers
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
