using AutoBogus;
using DotLanches.Pedidos.Domain.Entities;
using DotLanches.Pedidos.Domain.Interfaces.Repositories;
using DotLanches.Pedidos.Gateways;
using Moq;

namespace DotLanches.Pedidos.UnitTests.InterfaceAdapters.Gateways
{
    public class DatabaseGatewayTests
    {
        [Test]
        public async Task GetById_WhenPedidoExists_ShouldReturnPedido()
        {
            var pedidoId = Guid.NewGuid();
            var pedidoRepositoryMock = new Mock<IPedidoRepository>();
            var pedido = new AutoFaker<Pedido>().Generate();
            pedidoRepositoryMock
                .Setup(x => x.GetById(pedidoId))
                .ReturnsAsync(pedido);

            var databaseGateway = new DatabaseGateway(pedidoRepositoryMock.Object);

            var result = await databaseGateway.GetById(pedidoId);

            Assert.That(result, Is.EqualTo(pedido));
        }

        [Test]
        public async Task GetById_WhenPedidoDoesNotExist_ShouldReturnNull()
        {

            var pedidoId = Guid.NewGuid();
            var pedidoRepositoryMock = new Mock<IPedidoRepository>();
            pedidoRepositoryMock
                .Setup(x => x.GetById(pedidoId))
                .ReturnsAsync((Pedido?)null);

            var databaseGateway = new DatabaseGateway(pedidoRepositoryMock.Object);

            var result = await databaseGateway.GetById(pedidoId);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task Update_WhenCalled_ShouldCallRepository()
        {
            var pedidoId = Guid.NewGuid();
            var pedidoRepositoryMock = new Mock<IPedidoRepository>();
            var pedido = new AutoFaker<Pedido>().Generate();
            pedidoRepositoryMock
                .Setup(x => x.Update(pedido))
                .ReturnsAsync(pedido);

            var databaseGateway = new DatabaseGateway(pedidoRepositoryMock.Object);

            var result = await databaseGateway.Update(pedido);

            Assert.That(result, Is.EqualTo(pedido));

            pedidoRepositoryMock
                .Verify(x => x.Update(pedido), Times.Once);
        }
    }
}