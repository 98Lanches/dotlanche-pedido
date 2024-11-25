using AutoBogus;
using DotLanches.Pedidos.Domain.Entities;
using DotLanches.Pedidos.Domain.Interfaces.Clients;
using DotLanches.Pedidos.Gateways;
using Moq;

namespace DotLanches.Pedidos.UnitTests.InterfaceAdapters.Gateways
{
    public class ProducaoGatewayTests
    {
        [Test]
        public async Task StartProducaoPedido_WhenCalled_ShouldCallProducaoServiceClient()
        {
            var producaoServiceClientMock = new Mock<IProducaoServiceClient>();

            var producaoGateway = new ProducaoGateway(producaoServiceClientMock.Object);

            var pedido = new AutoFaker<Pedido>().Generate();

            await producaoGateway.StartProducaoPedido(pedido);

            producaoServiceClientMock
                .Verify(x => x.StartProducaoPedido(pedido), Times.Once());
        }
    }
}
