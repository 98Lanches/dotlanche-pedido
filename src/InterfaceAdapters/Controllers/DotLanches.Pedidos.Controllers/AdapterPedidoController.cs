using DotLanches.Pedidos.Application.UseCases;
using DotLanches.Pedidos.Domain.Entities;
using DotLanches.Pedidos.Domain.Interfaces.Clients;
using DotLanches.Pedidos.Domain.Interfaces.Repositories;
using DotLanches.Pedidos.Gateways;

namespace DotLanches.Pedidos.Controllers
{
    public class AdapterPedidoController
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPagamentoServiceClient _pagamentoServiceClient;

        public AdapterPedidoController(IPedidoRepository pedidoRepository, IPagamentoServiceClient pagamentoServiceClient)
        {
            _pedidoRepository = pedidoRepository;
            _pagamentoServiceClient = pagamentoServiceClient;
        }

        public async Task<Pedido> Create(Pedido pedido)
        {
            var pedidoGateway = new PedidoGateway(_pedidoRepository);
            var pagamentoGateway = new PagamentoGateway(_pagamentoServiceClient);

            var newPedido = await PedidoUseCases.Create(pedido, pedidoGateway, pagamentoGateway);

            return newPedido;
        }

        public async Task<Pedido?> GetById(Guid pedidoId)
        {
            var pedidoGateway = new PedidoGateway(_pedidoRepository);

            return await PedidoUseCases.GetById(pedidoId, pedidoGateway);
        }

        public async Task AcceptPagamento(Guid pedidoId, Guid registroPagamentoId)
        {
            var pedidoGateway = new PedidoGateway(_pedidoRepository);

            await PedidoUseCases.RegisterPagamentoForPedido(pedidoId, registroPagamentoId, pedidoGateway);
        }
    }
}