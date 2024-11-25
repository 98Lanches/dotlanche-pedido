using AutoBogus;
using DotLanches.Pedidos.Controllers;
using DotLanches.Pedidos.Domain.Entities;
using DotLanches.Pedidos.Domain.Interfaces.Clients;
using DotLanches.Pedidos.Domain.Interfaces.Repositories;
using Moq;

namespace DotLanches.Pedidos.UnitTests.InterfaceAdapters.InterfaceAdapters
{
    public class AdapterPedidoControllers
    {
        [Test]
        public async Task GetById_WhenPedidoExists_ShouldReturnIt()
        {
            var pagamentoServiceClientMock = new Mock<IPagamentoServiceClient>();
            var producaoServiceClientMock = new Mock<IProducaoServiceClient>();
            var pedidoRepositoryMock = new Mock<IPedidoRepository>();

            var idPedido = Guid.NewGuid();
            var pedido = new AutoFaker<Pedido>()
                .RuleFor(x => x.Id, idPedido)
                .Generate();
            pedidoRepositoryMock
                .Setup(x => x.GetById(idPedido))
                .ReturnsAsync(pedido);

            var adapterController = new AdapterPedidoController(pedidoRepositoryMock.Object, pagamentoServiceClientMock.Object, producaoServiceClientMock.Object);

            var result = await adapterController.GetById(idPedido);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task GetById_WhenPedidoDoesNotExist_ShouldNull()
        {
            var idPedido = Guid.NewGuid();

            var pagamentoServiceClientMock = new Mock<IPagamentoServiceClient>();
            var producaoServiceClientMock = new Mock<IProducaoServiceClient>();
            var pedidoRepositoryMock = new Mock<IPedidoRepository>();

            pedidoRepositoryMock
                .Setup(x => x.GetById(idPedido))
                .ReturnsAsync((Pedido?)null);

            var adapterController = new AdapterPedidoController(pedidoRepositoryMock.Object, pagamentoServiceClientMock.Object, producaoServiceClientMock.Object);

            var result = await adapterController.GetById(idPedido);

            Assert.That(result, Is.Null);
        }
    }
}