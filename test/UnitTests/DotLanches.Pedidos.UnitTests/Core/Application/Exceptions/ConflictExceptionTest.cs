using DotLanches.Pedidos.UseCases.Exceptions;

namespace DotLanches.Pedidos.UnitTests.Core.Application.Exceptions
{
    [TestFixture]
    public class ConflictExceptionTests
    {
        [Test]
        public void ConflictException_ShouldSetDefaultMessage()
        {
            var exception = new ConflictException();

            Assert.IsNotNull(exception);
            Assert.That(exception.Message, Is.EqualTo("Object already registered"));
        }
    }
}
