using DotLanches.Pedidos.Api.Mappers;
using DotLanches.Pedidos.Controllers;
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
    }
}