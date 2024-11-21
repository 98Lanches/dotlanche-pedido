using DotLanches.Pedidos.Domain.Entities;
using DotLanches.Pedidos.Domain.Exceptions;
using DotLanches.Pedidos.Domain.ValueObjects;

namespace DotLanches.Pedidos.UnitTests.Core.Domain.Entities
{
    public class PedidoTests
    { 
        [Test]
        public void CriarPedido_ComDadosValidos_DeveCriarPedidoCorretamente()
        {
            var combos = new List<Combo>
            {
                new Combo(new List<Guid> { Guid.Parse("01234567-89ab-cdef-0123-456789abcdef") }, 10.00m),
            };
            var createdAt = DateTime.Now;
            var clienteCpf = "12345678900";

            var pedido = new Pedido(createdAt, clienteCpf, combos);

            Assert.IsNotNull(pedido);
            Assert.That(pedido.TotalPrice, Is.EqualTo(10.00m));
            Assert.That(pedido.ClienteCpf, Is.EqualTo(clienteCpf));
            Assert.That(pedido.CreatedAt, Is.EqualTo(createdAt));
            Assert.That(pedido.Combos.Count(), Is.EqualTo(1));
        }

        [Test]
        public void CriarPedido_SemCombos_DeveLancarDomainValidationException()
        {
            var combos = new List<Combo>();  // Lista vazia de combos
            var createdAt = DateTime.Now;
            var clienteCpf = "12345678900";

            var ex = Assert.Throws<DomainValidationException>(() =>
                new Pedido(createdAt, clienteCpf, combos));

            Assert.That(ex.Message, Is.EqualTo("invalid value for O pedido deve conter pelo menos um combo!"));
        }

        [Test]
        public void CriarPedido_NullCombos_DeveLancarArgumentNullException()
        {
            var createdAt = DateTime.Now;
            var clienteCpf = "12345678900";

            var ex = Assert.Throws<ArgumentNullException>(() => new Pedido(createdAt, clienteCpf, null));

            Assert.That(ex.ParamName, Is.EqualTo("combos"));
        }
    }
}
