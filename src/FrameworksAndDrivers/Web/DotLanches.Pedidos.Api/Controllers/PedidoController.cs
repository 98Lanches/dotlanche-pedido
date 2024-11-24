using DotLanches.Pedidos.Api.Mappers;
using DotLanches.Pedidos.Controllers;
using DotLanches.Pedidos.Domain.Entities;
using DotLanches.Pedidos.Domain.Interfaces.Clients;
using DotLanches.Pedidos.Domain.Interfaces.Repositories;
using DotLanches.Pedidos.Presenters.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DotLanches.Pedidos.Api.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPagamentoServiceClient _pagamentoServiceClient;

        public PedidoController(IPedidoRepository pedidoRepository, IPagamentoServiceClient pagamentoServiceClient)
        {
            _pedidoRepository = pedidoRepository;
            _pagamentoServiceClient = pagamentoServiceClient;
        }

        /// <summary>
        /// Cria um novo Pedido
        /// </summary>
        /// <param name="pedidoDto">Dados do novo pedido</param>
        /// <returns>Pedido criado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreatePedidoRequest pedidoDto)
        {
            var adapterPedido = new AdapterPedidoController(_pedidoRepository, _pagamentoServiceClient);
            var newPedido = await adapterPedido.Create(pedidoDto.ToDomainModel());

            var response = newPedido.ToResponse();

            return new CreatedResult(string.Empty, response);
        }

        /// <summary>
        /// Obtém um pedido pelo seu ID
        /// </summary>
        /// <param name="idPedido">Id do pedido</param>
        [HttpGet]
        [ProducesResponseType(typeof(Pedido), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromQuery] Guid idPedido)
        {
            var adapterPedido = new AdapterPedidoController(_pedidoRepository, _pagamentoServiceClient);
            var pedido = await adapterPedido.GetById(idPedido);

            if (pedido == null)
                return NotFound("pedido not found");

            return Ok(pedido);
        }

        /// <summary>
        /// Recebe o pagamento de um pedido
        /// </summary>
        /// <param name="request">Dados do pagamento</param>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AcceptPagamento([FromBody] AcceptPagamentoRequest request)
        {
            var adapterPedido = new AdapterPedidoController(_pedidoRepository, _pagamentoServiceClient);
            await adapterPedido.AcceptPagamento(request.PedidoId, request.RegistroPagamentoId);

            return Ok();
        }
    }
}