using Moq;
using DotLanches.Pedidos.Domain.Entities;
using DotLanches.Pedidos.Domain.Interfaces.Gateways;
using DotLanches.Pedidos.Application.UseCases;
using DotLanches.Pedidos.Domain.ValueObjects;
using DotLanches.Pedidos.Domain.Enums;

namespace DotLanches.Pedidos.UnitTests.Core.Application.UseCases
{
    public class PedidoUseCasesTests
    {
        private Mock<IDatabaseGateway> _pedidoGatewayMock;
        private Mock<IPagamentoGateway> _pagamentoGatewayMock;

        [SetUp]
        public void SetUp()
        {
            _pedidoGatewayMock = new Mock<IDatabaseGateway>();
            _pagamentoGatewayMock = new Mock<IPagamentoGateway>();
        }

        [Test]
        public async Task Create_ValidPedido_ReturnsPedido()
        {
            var combos = new List<Combo> { new Combo(new List<Guid> { Guid.Parse("01234567-89ab-cdef-0123-456789abcdef") }, 12) };
            var pedido = new Pedido(DateTime.Now, "12345678901", combos, TipoPagamento.QrCode);
            _pedidoGatewayMock.Setup(pg => pg.Add(It.IsAny<Pedido>())).Returns(Task.CompletedTask);

            var result = await PedidoUseCases.Create(pedido, _pedidoGatewayMock.Object, _pagamentoGatewayMock.Object);

            Assert.IsNotNull(result);
            Assert.AreEqual(pedido, result);
            _pedidoGatewayMock.Verify(pg => pg.Add(It.Is<Pedido>(p => p == pedido)), Times.Once);
        }

        [Test]
        public void Create_NullPedido_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => 
                await PedidoUseCases.Create(null, _pedidoGatewayMock.Object, _pagamentoGatewayMock.Object));
        }
    }
}
