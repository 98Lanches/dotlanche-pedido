using AutoBogus;
using DotLanches.Pedidos.Application.UseCases;
using DotLanches.Pedidos.Domain.Entities;
using DotLanches.Pedidos.Domain.Enums;
using DotLanches.Pedidos.Domain.Interfaces.Gateways;
using DotLanches.Pedidos.Domain.ValueObjects;
using DotLanches.Pedidos.UseCases.Exceptions;
using Moq;

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

        [Test]
        public async Task RegisterPagamentoForPedido_WhenPedidoExists_ShouldCallUpdateAndStartProducaoPedido()
        {
            var idPedido = Guid.NewGuid();
            var idRegistroPagamento = Guid.NewGuid();

            var pedido = new AutoFaker<Pedido>()
                .RuleFor(x => x.Id, idPedido)
                .Generate();

            var databaseGatewayMock = new Mock<IDatabaseGateway>();
            databaseGatewayMock
                .Setup(x => x.GetById(idPedido))
                .ReturnsAsync(pedido);

            var producaoGatewayMock = new Mock<IProducaoGateway>();

            await PedidoUseCases.RegisterPagamentoForPedido(idPedido, idRegistroPagamento, databaseGatewayMock.Object, producaoGatewayMock.Object);

            databaseGatewayMock
                .Verify(x =>
                    x.Update(It.Is<Pedido>(x =>
                        x.Id == idPedido
                        && x.Pagamento.Accepted == true
                        && x.Pagamento.Id == idRegistroPagamento)),
                Times.Once());

            producaoGatewayMock
                .Verify(x =>
                    x.StartProducaoPedido(It.Is<Pedido>(x =>
                        x.Id == idPedido)),
                Times.Once());
        }

        [Test]
        public void RegisterPagamentoForPedido_WhenPedidoDoesNotExist_ThrowUseCaseException()
        {
            var idPedido = Guid.NewGuid();
            var idRegistroPagamento = Guid.NewGuid();

            var databaseGatewayMock = new Mock<IDatabaseGateway>();
            databaseGatewayMock
                .Setup(x => x.GetById(idPedido))
                .ReturnsAsync((Pedido?)null);

            var producaoGatewayMock = new Mock<IProducaoGateway>();

            var exception = Assert.ThrowsAsync<UseCaseException>(async () =>
                await PedidoUseCases.RegisterPagamentoForPedido(idPedido,
                                                                idRegistroPagamento,
                                                                databaseGatewayMock.Object,
                                                                producaoGatewayMock.Object));

            Assert.That(exception!.Message, Is.EqualTo($"pedido {idPedido} was not found!"));
        }

        [Test]
        public async Task GetById_WhenPedidoExists_ShouldReturnIt()
        {
            var idPedido = Guid.NewGuid();

            var pedido = new AutoFaker<Pedido>()
                .RuleFor(x => x.Id, idPedido)
                .Generate();

            var databaseGatewayMock = new Mock<IDatabaseGateway>();
            databaseGatewayMock
                .Setup(x => x.GetById(idPedido))
                .ReturnsAsync(pedido);

            var result = await PedidoUseCases.GetById(idPedido, databaseGatewayMock.Object);

            Assert.That(result, Is.EqualTo(pedido));
        }

        [Test]
        public async Task GetById_WhenPedidoDoesNotExist_ShouldReturnNull()
        {
            var idPedido = Guid.NewGuid();

            var databaseGatewayMock = new Mock<IDatabaseGateway>();
            databaseGatewayMock
                .Setup(x => x.GetById(idPedido))
                .ReturnsAsync((Pedido?)null);

            var result = await PedidoUseCases.GetById(idPedido, databaseGatewayMock.Object);

            Assert.That(result, Is.Null);
        }
    }
}