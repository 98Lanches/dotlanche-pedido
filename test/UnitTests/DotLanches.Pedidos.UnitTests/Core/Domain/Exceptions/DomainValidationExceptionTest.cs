using DotLanches.Pedidos.Domain.Exceptions;

namespace DotLanches.Pedidos.Tests.Domain.Exceptions
{
    [TestFixture]
    public class DomainValidationExceptionTests
    {
        [Test]
        public void DomainValidationException_ShouldFormatMessageCorrectly()
        {
            var propertyName = "TotalPrice";
            var exception = new DomainValidationException(propertyName);
            
            Assert.That(exception.Message, Is.EqualTo("invalid value for TotalPrice!"));
        }
    }
}
