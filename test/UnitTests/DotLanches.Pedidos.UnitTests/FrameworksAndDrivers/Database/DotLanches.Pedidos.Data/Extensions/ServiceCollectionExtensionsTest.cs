using DotLanches.Pedidos.DataMongo.Extensions;
using DotLanches.Pedidos.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Moq;

namespace DotLanches.Pedidos.UnitTests.FrameworksAndDrivers.Database.DotLanches.Pedidos.Data.Extensions
{
    [TestFixture]
    public class ServiceCollectionExtensionsTests
    {
        private Mock<IConfiguration> _configurationMock;
        private Mock<IConfigurationSection> _configurationSectionMock;
        private IServiceCollection _services;

        [SetUp]
        public void SetUp()
        {
            _configurationMock = new Mock<IConfiguration>();
            _configurationSectionMock = new Mock<IConfigurationSection>();
            _configurationSectionMock.Setup(s => s.Value).Returns("mongodb://localhost:27017");
            _configurationMock.Setup(c => c.GetSection("ConnectionStrings:DefaultConnection")).Returns(_configurationSectionMock.Object);

            _services = new ServiceCollection();
        }

        [Test]
        public void ConfigureDatabase_ShouldRegisterMongoClient()
        {
            _services.ConfigureDatabase(_configurationMock.Object);
            var mongoClientDescriptor = _services.FirstOrDefault(d => d.ServiceType == typeof(MongoClient));
            Assert.IsNotNull(mongoClientDescriptor, "MongoClient should be registered.");
            Assert.That(mongoClientDescriptor.Lifetime, Is.EqualTo(ServiceLifetime.Singleton), "MongoClient should be registered as singleton.");
        }

        [Test]
        public void ConfigureDatabase_ShouldRegisterDatabase()
        {
            _services.ConfigureDatabase(_configurationMock.Object);
            var mongoDatabaseDescriptor = _services.FirstOrDefault(d => d.ServiceType == typeof(IMongoDatabase));
            Assert.IsNotNull(mongoDatabaseDescriptor, "IMongoDatabase should be registered.");
            Assert.That(mongoDatabaseDescriptor.Lifetime, Is.EqualTo(ServiceLifetime.Singleton), "IMongoDatabase should be registered as singleton.");
        }

        [Test]
        public void ConfigureDatabase_ShouldRegisterPedidoRepository()
        {
            _services.ConfigureDatabase(_configurationMock.Object);
            var pedidoRepositoryDescriptor = _services.FirstOrDefault(d => d.ServiceType == typeof(IPedidoRepository));
            Assert.IsNotNull(pedidoRepositoryDescriptor, "IPedidoRepository should be registered.");
            Assert.That(pedidoRepositoryDescriptor.Lifetime, Is.EqualTo(ServiceLifetime.Scoped), "IPedidoRepository should be registered as scoped.");
        }

        [Test]
        public void RegisterConventions_ShouldRegisterGuidAndEnumConventions()
        {
            _services.ConfigureDatabase(_configurationMock.Object);
            var conventions = ConventionRegistry.Lookup(typeof(BsonDocument)).Conventions;
            Assert.IsTrue(conventions.Any(c => c is EnumRepresentationConvention), "EnumRepresentationConvention should be registered.");
        }
    }
}
