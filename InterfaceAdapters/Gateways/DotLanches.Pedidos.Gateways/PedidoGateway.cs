using DotLanches.Pedidos.Domain.Entities;
using DotLanches.Pedidos.Domain.Interfaces.Gateways;
using DotLanches.Pedidos.Domain.Interfaces.Repositories;

namespace DotLanches.Pedidos.Gateways
{
    public class PedidoGateway : IPedidoGateway
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoGateway(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task Add(Pedido pedido) => await _pedidoRepository.Add(pedido);

        public async Task<Pedido?> GetById(Guid id) => await _pedidoRepository.GetById(id);

        public async Task<Pedido> Update(Pedido pedido) => await _pedidoRepository.Update(pedido);
    }
}
