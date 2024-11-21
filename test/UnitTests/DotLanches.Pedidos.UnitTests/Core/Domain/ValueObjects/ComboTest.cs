using DotLanches.Pedidos.Domain.Exceptions;
using DotLanches.Pedidos.Domain.ValueObjects;

namespace DotLanches.Pedidos.UnitTests.Core.Domain.ValueObjects
{
    public class ComboTests
    {
        [Test]
        public void Construtor_DeveLancarExcecao_QuandoIdProdutoForGuidVazio()
        {
            var idProduto = Guid.Empty;
            var preco = 10.0m;

            var ex = Assert.Throws<DomainValidationException>(() => new Combo(new List<Guid>{ idProduto }, preco));
            Assert.That(ex.Message, Is.EqualTo("invalid value for idsProduto!"));
        }

        [Test]
        public void Construtor_DeveLancarExcecao_QuandoPrecoFor0()
        {
            var idProduto = Guid.Parse("01234567-89ab-cdef-0123-456789abcdef");
            var preco = 0.00m;

            var ex = Assert.Throws<DomainValidationException>(() => new Combo(new List<Guid>{ idProduto }, preco));
            Assert.That(ex.Message, Is.EqualTo("invalid value for precoTotal!"));
        }

        [Test]
        public void Construtor_DeveLancarExcecao_QuandoPrecoForNegativo()
        {
            var idProduto = Guid.Parse("01234567-89ab-cdef-0123-456789abcdef");
            var preco = -10.00m;

            var ex = Assert.Throws<DomainValidationException>(() => new Combo(new List<Guid>{ idProduto }, preco));
            Assert.That(ex.Message, Is.EqualTo("invalid value for precoTotal!"));
        }

        [Test]
        public void Construtor_DeveLancarExcecao_QuandoIdsProdutoForNulo()
        {
            IEnumerable<Guid> idsProduto = null;
            var preco = 10.0m;

            var ex = Assert.Throws<DomainValidationException>(() => new Combo(idsProduto, preco));
            Assert.That(ex.Message, Is.EqualTo("invalid value for idsProduto!"));
        }

        [Test]
        public void Construtor_DeveLancarExcecao_QuandoIdsProdutoForVazio()
        {
            var idsProduto = new List<Guid>();
            var preco = 10.0m;

            var ex = Assert.Throws<DomainValidationException>(() => new Combo(idsProduto, preco));
            Assert.That(ex.Message, Is.EqualTo("invalid value for idsProduto!"));
        }

        [Test]
        public void Construtor_DeveLancarExcecao_QuandoNumeroDeProdutosForMaiorQue4()
        {
            var idsProduto = new List<Guid>
            {
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()
            };
            var preco = 10.0m;

            var ex = Assert.Throws<DomainValidationException>(() => new Combo(idsProduto, preco));
            Assert.That(ex.Message, Is.EqualTo("invalid value for idsProduto!"));
        }

        [Test]
        public void Construtor_DeveCriarCombo_QuandoIdsProdutoValidosEPrecoPositivo()
        {
            var idsProduto = new List<Guid>
            {
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()
            };
            var preco = 15.0m;

            var combo = new Combo(idsProduto, preco);

            Assert.That(combo.IdsProduto.Count(), Is.EqualTo(3));
            Assert.That(combo.PrecoTotal, Is.EqualTo(15.0m));
        }
    }
}
