using DotLanches.Pedidos.DataMongo.Exceptions;
using DotLanches.Pedidos.DataMongo.Repositories;
using DotLanches.Pedidos.Domain.Entities;
using DotLanches.Pedidos.Domain.ValueObjects;
using MongoDB.Driver;
using Moq;

namespace DotLanches.Pedidos.UnitTests.FrameworksAndDrivers.Database.DotLanches.Pedidos.Data.Repositories
{
    [TestFixture]
    public class PedidoRepositoryTests
    {
        private Mock<IMongoDatabase> _mockDatabase;
        private Mock<IMongoCollection<Pedido>> _mockCollection;
        private PedidoRepository _pedidoRepository;

        [SetUp]
        public void Setup()
        {
            _mockDatabase = new Mock<IMongoDatabase>();
            _mockCollection = new Mock<IMongoCollection<Pedido>>();
            _mockDatabase.Setup(db => db.GetCollection<Pedido>("Pedidos", null))
                         .Returns(_mockCollection.Object);

            _pedidoRepository = new PedidoRepository(_mockDatabase.Object);
        }

        [Test]
        public async Task Add_ShouldInsertPedido()
        {
            var combos = new List<Combo>
            {
                new Combo (Guid.Parse("01234567-89ab-cdef-0123-456789abcdef"), 10.00m),
                new Combo (Guid.Parse("03234567-89ab-c2ef-0123-456789abcdee"), 20.00m)
            };
            var createdAt = DateTime.Now;
            var clienteCpf = "12345678900";

            var pedido = new Pedido(createdAt, clienteCpf, combos);
            _mockCollection.Setup(c => c.InsertOneAsync(pedido, null, default))
                           .Returns(Task.CompletedTask);

            await _pedidoRepository.Add(pedido);

            _mockCollection.Verify(c => c.InsertOneAsync(pedido, null, default), Times.Once);
        }

        [Test]
        public async Task GetById_ShouldReturnPedido_WhenPedidoExists_WithAnotherApproach()
        {
            // Definindo os combos e o pedido a ser retornado
            var combos = new List<Combo>
            {
                new Combo(Guid.Parse("01234567-89ab-cdef-0123-456789abcdef"), 10.00m),
                new Combo(Guid.Parse("03234567-89ab-c2ef-0123-456789abcdee"), 20.00m)
            };
            var createdAt = DateTime.Now;
            var clienteCpf = "12345678900";
    
            var pedido = new Pedido(createdAt, clienteCpf, combos);
            var mockCursor = new Mock<IAsyncCursor<Pedido>>();

            mockCursor.Setup(c => c.MoveNext(It.IsAny<CancellationToken>())).Returns(true);  // Um movimento para o próximo item
            mockCursor.Setup(c => c.Current).Returns(new List<Pedido> { pedido });  // Definir que o pedido será retornado

            // Configurar o mock da coleção para retornar o mock do cursor
            _mockCollection
                .Setup(c => c.FindAsync(It.IsAny<FilterDefinition<Pedido>>(),
                    It.IsAny<FindOptions<Pedido>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockCursor.Object);

            // Chama o método GetById no repositório, buscando o pedido
            var result = await _pedidoRepository.GetById(pedido.Id);
            
            Assert.NotNull(result);
            Assert.AreEqual(pedido.Id, result.Id);
            Assert.AreEqual(clienteCpf, result.ClienteCpf);  // Verifica se o CPF do cliente também é o esperado
        }


        [Test]
        public async Task GetById_ShouldReturnNull_WhenPedidoDoesNotExist()
        {
            var pedidoId = Guid.NewGuid();
            var mockCursor = new Mock<IAsyncCursor<Pedido>>();

            mockCursor.SetupSequence(c => c.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(false);
            mockCursor.Setup(c => c.Current).Returns(Enumerable.Empty<Pedido>());
            
            _mockCollection
                .Setup(c => c.FindAsync(It.IsAny<FilterDefinition<Pedido>>(), It.IsAny<FindOptions<Pedido>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockCursor.Object);

            var result = await _pedidoRepository.GetById(pedidoId);

            Assert.IsNull(result);
        }

        [Test]
        public async Task Update_ShouldReplacePedido_WhenPedidoExists()
        {
            var combos = new List<Combo>
            {
                new Combo (Guid.Parse("01234567-89ab-cdef-0123-456789abcdef"), 10.00m)
            };
            var createdAt = DateTime.Now;
            var clienteCpf = "12345678900";
            var pedido = new Pedido(createdAt, clienteCpf, combos);

            var updateResult = new ReplaceOneResult.Acknowledged(1, 1, pedido.Id);
            _mockCollection
                .Setup(c => c.ReplaceOneAsync(
                    It.IsAny<FilterDefinition<Pedido>>(),
                    pedido,
                    It.IsAny<ReplaceOptions>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(updateResult);

            var result = await _pedidoRepository.Update(pedido);

            Assert.AreEqual(pedido, result);
        }

        [Test]
        public void Update_ShouldThrowEntityNotFoundException_WhenPedidoDoesNotExist()
        {
            var combos = new List<Combo>
            {
                new Combo (Guid.Parse("01234567-89ab-cdef-0123-456789abcdef"), 10.00m)
            };
            var createdAt = DateTime.Now;
            var clienteCpf = "12345678900";
            var pedido = new Pedido(createdAt, clienteCpf, combos);

            var updateResult = new ReplaceOneResult.Acknowledged(0, 0, null);
            _mockCollection
                .Setup(c => c.ReplaceOneAsync(
                    It.IsAny<FilterDefinition<Pedido>>(),
                    pedido,
                    It.IsAny<ReplaceOptions>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(updateResult);

            Assert.ThrowsAsync<EntityNotFoundException>(() => _pedidoRepository.Update(pedido));
        }
    }
}
