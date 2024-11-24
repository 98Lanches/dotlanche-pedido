using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace DotLanches.Pedidos.BDDTests.Drivers
{
    public static class PagamentoServiceDriver
    {
        private static WireMockServer? pagamentoServiceServerMock;

        public static void SetWiremockServer(WireMockServer wireMockServer)
        {
            pagamentoServiceServerMock = wireMockServer;
        }

        public static void SetupSuccessfullQrCodeResponse()
        {
            if (pagamentoServiceServerMock == null || !pagamentoServiceServerMock.IsStarted)
                throw new InvalidOperationException("Wiremock server needs to be set before setting it up!");

            var response = @$"
                {{
                    ""operationSuccessful"": true,
                    ""registroPagamentoId"": ""{Guid.NewGuid()}"",
                    ""pedidoId"": ""{Guid.NewGuid()}"",
                    ""isAccepted"": false,
                    ""registeredTime"": ""2024-11-24T15:42:07.1163249-03:00"",
                    ""providerData"": {{
                        ""QR_CODE_IMG"": ""iVBORw0KGgoAAAANSUhEUgAAAUoAAAFKAQAAAABTUiuoAAABcElEQVR4nO2ZS67DIAwAkXqAHKlXz5F6gEp5+Ifb9DXqzl4MizaBYTOWiYFx/Nr2AYoBDGAAAxjoYWB4u83nh/wczzHuj+i+g7YyoC9CjbEJpX02KUZB+xiQIOrYvumYBlZmzmCDdjXgMY3AgjY3YE/xCtrRQCyaMw2f0udpeLW+glYZWAVJRDdCfFG7gBYZyObJF1XJSwNtY0CSzwJrP5uO+STQZgZ0lbTurCFXSQnayoBF8tCxOSnjbNu19/UVtIOB5GN/FrkI2srA/J+ABTY/cfZ0Kh9Byw1E8eEZmMnnuQjaysCKpKF7ZqVu0kBbGfC6Q2MqH7fk/zkhAS03kEdTsnyeNtegnQxk09G3RfP7sQdojYEMoubi5eUKaLmBvLh8vQ9b3z7QXgbOdX6E+uOQCrSVATuzX+jFrRlotQHbUksa2kmVLZ+gvQysRXPdh/lR8EdBAlpuIAuS3JCtaxbQXgZ+aaAYwAAGMICBegN/TnpNNQzdElMAAAAASUVORK5CYII=""
                    }}
                }}
                ";

            pagamentoServiceServerMock
                .Given(
                    Request.Create()
                    .UsingPost()
                    .WithPath("/api/pagamentos")
                 )
                .RespondWith(
                    Response.Create()
                    .WithStatusCode(System.Net.HttpStatusCode.OK)
                    .WithHeader("Content-Type", "application/json")
                    .WithBody(response)
                );
        }
    }
}