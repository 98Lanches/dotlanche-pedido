using DotLanches.Pedidos.BDDTests.Drivers;
using Reqnroll;
using WireMock.Server;

namespace DotLanches.Pedidos.BDDTests.Hooks
{
    [Binding]
    public class PagamentoServiceHooks
    {
        public const int ServerPort = 8082;
        private static WireMockServer? wiremockServer;

        [BeforeFeature]
        public static void StartMockedServer()
        {
            wiremockServer = WireMockServer.Start(ServerPort);
            PagamentoServiceDriver.SetWiremockServer(wiremockServer);
        }

        [AfterFeature]
        public static void StopMockedServer()
        {
            wiremockServer?.Stop();
            wiremockServer?.Dispose();
        }
    }
}
