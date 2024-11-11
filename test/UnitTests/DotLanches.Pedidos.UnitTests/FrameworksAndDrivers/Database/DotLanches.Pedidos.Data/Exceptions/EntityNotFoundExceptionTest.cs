using DotLanches.Pedidos.DataMongo.Exceptions;

namespace DotLanches.Pedidos.UnitTests.FrameworksAndDrivers.Database.DotLanches.Pedidos.Data.Exceptions
{
    [TestFixture]
    public class EntityNotFoundExceptionTests
    {
        [Test]
        public void EntityNotFoundException_ShouldUseDefaultMessage_WhenNoMessageIsProvided()
        {
            var exception = new EntityNotFoundException();
            Assert.That(exception.Message, Is.EqualTo("Entity not found!"));
        }

        [Test]
        public void EntityNotFoundException_ShouldUseProvidedMessage_WhenMessageIsProvided()
        {
            var customMessage = "Custom entity not found message";
            var exception = new EntityNotFoundException(customMessage);
            Assert.That(exception.Message, Is.EqualTo(customMessage));
        }

        [Test]
        public void EntityNotFoundException_ShouldBeOfTypeException()
        {
            var exception = new EntityNotFoundException();
            Assert.That(exception, Is.InstanceOf<Exception>());
        }
    }
}
