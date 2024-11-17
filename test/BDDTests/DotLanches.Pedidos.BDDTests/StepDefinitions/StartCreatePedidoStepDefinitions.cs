using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using DotLanches.Pedidos.BDDTests.Setup;
using NUnit.Framework;
using Reqnroll;
namespace DotLanches.Pedidos.BDDTests.StepDefinitions;

[Binding]
public class StartCreatePedidoStepDefinitions
{
    private readonly WebApiFactory _apiFactory;
    private readonly HttpClient _apiClient;
    private readonly JsonSerializerOptions _jsonOptions;
    private HttpResponseMessage _response;
    private object _payload;

    public StartCreatePedidoStepDefinitions(WebApiFactory apiFactory)
    {
        _apiFactory = apiFactory;
        _apiClient = _apiFactory.CreateClient();
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        _jsonOptions.Converters.Add(new JsonStringEnumConverter());
    }

    [Given(@"que a API está em execução")]
    public void GivenQueAApiEstaEmExecucao()
    {
        Assert.NotNull(_apiFactory);
    }

    [Given(@"que o endpoint ""(.*)"" está disponível")]
    public async Task GivenQueOEndpointEstaDisponivel(string endpoint)
    {
        var request = new HttpRequestMessage(HttpMethod.Options, endpoint);
        _response = await _apiClient.SendAsync(request);

        if (_response.StatusCode != HttpStatusCode.OK)
        {
            throw new Exception($"Endpoint {endpoint} não está disponível. Status code: {_response.StatusCode}");
        }
    }

    [Given(@"que o payload do pedido é válido")]
    public void GivenQueOPayloadDoPedidoEValido(Table table)
    {
        _payload = table.CreateInstance<object>();
    }

    [When(@"envio uma requisição POST para ""(.*)"" com o payload")]
    public async Task WhenEnvioUmaRequisicaoPostParaComOPayload(string endpoint)
    {
        _response = await _apiClient.PostAsJsonAsync(endpoint, _payload, _jsonOptions);

        if (_response == null)
        {
            throw new Exception("A requisição POST não retornou resposta.");
        }
    }

    [Then(@"a resposta deve ter o status (.*) Created")]
    public void ThenARespostaDeveTerOStatusCreated(int expectedStatusCode)
    {
        Assert.Equals(expectedStatusCode, (int)_response.StatusCode);
    }

    [Then(@"a resposta deve conter o ID do pedido criado")]
    public async Task ThenARespostaDeveConterOidDoPedidoCriado()
    {
        var responseBody = await _response.Content.ReadFromJsonAsync<JsonElement>(_jsonOptions);
        
        if (!responseBody.TryGetProperty("pedidoId", out var pedidoId))
        {
            throw new Exception("A resposta não contém a propriedade 'pedidoId'.");
        }

        Assert.NotNull(pedidoId.GetString());
        Console.WriteLine($"Pedido criado com sucesso: ID {pedidoId}");
    }
}
