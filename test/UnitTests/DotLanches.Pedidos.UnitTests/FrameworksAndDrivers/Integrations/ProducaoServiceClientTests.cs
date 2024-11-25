using AutoBogus;
using DotLanches.Pedidos.Domain.Entities;
using DotLanches.Pedidos.Domain.ValueObjects;
using DotLanches.Pedidos.Gateways.Exceptions;
using DotLanches.Pedidos.Integrations;
using DotLanches.Pedidos.Integrations.DTOs;
using RichardSzalay.MockHttp;
using System.Net;
using System.Text.Json;

namespace DotLanches.Pedidos.UnitTests.FrameworksAndDrivers.Integrations
{
    public class ProducaoServiceClientTests
    {
        [Test]
        public async Task StartProducaoPedido_WhenResponseIsSuccessfulAndValid_ShouldReturnTrue()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();

            var successfulResponse = new AutoFaker<StartProducaoPedidoResponse>().Generate();
            successfulResponse.Success = true;

            mockHttp
                .When("/api/producao")
                .Respond(HttpStatusCode.OK, "application/json", JsonSerializer.Serialize(successfulResponse));

            var httpClient = new HttpClient(mockHttp)
            {
                BaseAddress = new Uri("http://test.com")
            };
            var serviceClient = new ProducaoServiceClient(httpClient);

            var comboFaker = new AutoFaker<Combo>();
            var pedido = new AutoFaker<Pedido>()
                .RuleFor(x => x.Combos, comboFaker.Generate(2))
                .Generate();

            // Act
            var result = await serviceClient.StartProducaoPedido(pedido);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void StartProducaoPedido_WhenResponseStatusCodeIsNotSuccess_ShouldThrowException()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();

            mockHttp
                .When("/api/producao")
                .Respond(HttpStatusCode.ServiceUnavailable, "application/json", "ERROR");

            var httpClient = new HttpClient(mockHttp)
            {
                BaseAddress = new Uri("http://test.com")
            };
            var serviceClient = new ProducaoServiceClient(httpClient);

            var pedido = new AutoFaker<Pedido>();

            // Act - Assert
            var exception = Assert.ThrowsAsync<ServiceIntegrationException>(async () => 
                await serviceClient.StartProducaoPedido(pedido));

            Assert.That(exception!.Message, Is.EqualTo("Could not integrate with service ProducaoApi. Error: Could not connect to service. status code returned ServiceUnavailable"));
        }

        [Test]
        public async Task StartProducaoPedido_WhenResponseIsUnsuccessful_ShouldReturnFalse()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();

            var unsucessfulResponse = new AutoFaker<StartProducaoPedidoResponse>().Generate();
            unsucessfulResponse.Success = false;

            mockHttp
                .When("/api/producao")
                .Respond(HttpStatusCode.OK, "application/json", JsonSerializer.Serialize(unsucessfulResponse));

            var httpClient = new HttpClient(mockHttp)
            {
                BaseAddress = new Uri("http://test.com")
            };
            var serviceClient = new ProducaoServiceClient(httpClient);

            var pedido = new AutoFaker<Pedido>();

            // Act
            var result = await serviceClient.StartProducaoPedido(pedido);

            // Assert
            Assert.That(result, Is.False);
        }
    }
}