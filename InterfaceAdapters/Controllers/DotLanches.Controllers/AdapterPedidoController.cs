using DotLanches.Application.UseCases;
using DotLanches.Domain.Entities;
using DotLanches.Domain.Interfaces.Repositories;
using DotLanches.Gateways;

namespace DotLanches.Controllers
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

        public async Task<IEnumerable<Pedido>> GetPedidosQueue()
        {
            var pedidoGateway = new PedidoGateway(_pedidoRepository);
            var pedidoList = await PedidoUseCases.GetPedidosQueue(pedidoGateway);
            return pedidoList;
        }

        public async Task<Pedido> UpdateStatus(Guid idPedido, EStatus status)
        {
            var pedidoGateway = new PedidoGateway(_pedidoRepository);
            var updatedPedido = await PedidoUseCases.UpdateStatusOfSelectedPedido(idPedido, status, pedidoGateway);
            return updatedPedido;
        }
    }
}
