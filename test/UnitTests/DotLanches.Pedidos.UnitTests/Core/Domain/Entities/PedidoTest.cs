using DotLanches.Pedidos.Domain.Entities;
using DotLanches.Pedidos.Domain.Exceptions;
using DotLanches.Pedidos.Domain.ValueObjects;

namespace DotLanches.Pedidos.UnitTests.Core.Domain.Entities
{
    [TestFixture]
    public class PedidoTests : Pedido
    {   
        public PedidoTests() 
        {
        }
        public PedidoTests(DateTime createdAt, string? clienteCpf, IEnumerable<Combo> combos)
        : base(createdAt, clienteCpf, combos)
        {
        }

        protected override decimal CalculateTotalPrice()
        {
            return 0.00m;
        }

        [Test]
        public void CriarPedido_ComDadosValidos_DeveCriarPedidoCorretamente()
        {
            var combos = new List<Combo>
            {
                new Combo (Guid.Parse("01234567-89ab-cdef-0123-456789abcdef"), 10.00m),
                new Combo (Guid.Parse("03234567-89ab-c2ef-0123-456789abcdee"), 20.00m )
            };
            var createdAt = DateTime.Now;
            var clienteCpf = "12345678900";

            var pedido = new Pedido(createdAt, clienteCpf, combos);

            Assert.IsNotNull(pedido);
            Assert.That(pedido.TotalPrice, Is.EqualTo(30.00m));
            Assert.That(pedido.ClienteCpf, Is.EqualTo(clienteCpf));
            Assert.That(pedido.CreatedAt, Is.EqualTo(createdAt));
            Assert.That(pedido.Combos.Count(), Is.EqualTo(2));
        }

        [Test]
        public void CriarPedido_SemCombos_DeveLancarDomainValidationException()
        {
            var combos = new List<Combo>();
            var createdAt = DateTime.Now;
            var clienteCpf = "12345678900";

            var ex = Assert.Throws<DomainValidationException>(() =>
                new Pedido(createdAt, clienteCpf, combos));
            
            Assert.That(ex.Message, Is.EqualTo("invalid value for O pedido deve conter pelo menos um combo.!"));
        }

        [Test]
        public void CriarPedido_ComTotalPriceZero_DeveLancarDomainValidationException()
        {
            // Arrange
            var combos = new List<Combo>
            {
                new Combo(Guid.Parse("01234567-89ab-cdef-0123-456789abcdef"), 10.00m),
                new Combo(Guid.Parse("03234567-89ab-c2ef-0123-456789abcdee"), 20.00m)
            };
            var createdAt = DateTime.Now;
            var clienteCpf = "12345678900";

            var ex = Assert.Throws<DomainValidationException>(() =>
                new PedidoTests(createdAt, clienteCpf, combos));
            
            Assert.That(ex.Message, Is.EqualTo("invalid value for O preço total do pedido deve ser maior que zero.!"));
        }

        [Test]
        public void CriarPedido_NullCombos_DeveLancarArgumentNullException()
        {
            var createdAt = DateTime.Now;
            var clienteCpf = "12345678900";

            Assert.Throws<ArgumentNullException>(() => new Pedido(createdAt, clienteCpf, null));
        }
    }
}
