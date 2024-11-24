using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using DotLanches.Pedidos.BDDTests.Drivers;
using DotLanches.Pedidos.BDDTests.Setup;
using DotLanches.Pedidos.Domain.Enums;
using DotLanches.Pedidos.Presenters.Dtos;
using NUnit.Framework;
using Reqnroll;
namespace DotLanches.Pedidos.BDDTests.StepDefinitions;

[Binding]
public class StartCreatePedidoStepDefinitions
{
    private readonly WebApiFactory _apiFactory;
    private readonly HttpClient _apiClient;
    private readonly JsonSerializerOptions _jsonOptions;
    private HttpResponseMessage? _httpResponse;
    private CreatePedidoResponse? _response;
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

    [Given(@"o servico de pagamentos esta funcionando corretamente")]
    public void GivenServicoDePagamentoEstaFuncionando()
    {
        PagamentoServiceDriver.SetupSuccessfullQrCodeResponse();
    }

    [Given(@"que o payload do pedido é válido")]
    public void GivenQueOPayloadDoPedidoEValido(Table table)
    {
        _payload = new CreatePedidoRequest()
        {
            ClienteCpf = table.Rows[0]["ClienteCpf"],
            TipoPagamento = TipoPagamento.QrCode,
            Combos = table.Rows.Select(r => new ComboDto()
            {
                IdsProduto = new List<Guid> { Guid.Parse(r["ProdutoId"]) },
                PrecoTotal = decimal.Parse(r["Preco"])
            }).ToList()
        };

    }

    [When(@"envio uma requisição POST para ""(.*)"" com o payload")]
    public async Task WhenEnvioUmaRequisicaoPostParaComOPayload(string endpoint)
    {
        _httpResponse = await _apiClient.PostAsJsonAsync(endpoint, _payload, _jsonOptions);

        if (_httpResponse == null)
        {
            throw new Exception("A requisição POST não retornou resposta.");
        }
    }

    [Then(@"a resposta deve ter o status (.*) Created")]
    public async Task ThenARespostaDeveTerOStatusCreated(int expectedStatusCode)
    {
        Assert.That((int)_httpResponse!.StatusCode, Is.EqualTo(expectedStatusCode));
        _response ??= await _httpResponse.Content.ReadFromJsonAsync<CreatePedidoResponse>(_jsonOptions); 
    }

    [Then(@"a resposta deve conter o ID do pedido criado")]
    public async Task ThenARespostaDeveConterOidDoPedidoCriado()
    {
        _response ??= await _httpResponse!.Content.ReadFromJsonAsync<CreatePedidoResponse>(_jsonOptions); 
        Assert.That(_response!.PedidoId, Is.Not.Empty);
    }

    [Then(@"a resposta deve conter o QR code para pagamento")]
    public async Task ThenARespostaDeveConterQrCode()
    {
        _response ??= await _httpResponse!.Content.ReadFromJsonAsync<CreatePedidoResponse>(_jsonOptions); 
        Assert.That(_response!.PagamentoInformation, Is.Not.Null);
    }
}
