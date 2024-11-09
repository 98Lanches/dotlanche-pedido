using DotLanches.Pedidos.Domain.Exceptions;
using DotLanches.Pedidos.Domain.ValueObjects;

namespace DotLanches.Pedidos.Tests.Domain.ValeObjects
{
    [TestFixture]
    public class ComboTests
    {
        [Test]
        public void Construtor_DeveLancarExcecao_QuandoIdProdutoForGuidVazio()
        {
            var idProduto = Guid.Empty;
            var preco = 10.0m;

            var ex = Assert.Throws<DomainValidationException>(() => new Combo(idProduto, preco));
            Assert.That(ex.Message, Is.EqualTo("invalid value for idProduto!"));
        }
    }
}
