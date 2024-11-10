using DotLanches.Pedidos.Api.Dtos;
using DotLanches.Pedidos.Api.Mappers;
using DotLanches.Pedidos.Controllers;
using DotLanches.Pedidos.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DotLanches.Pedidos.Api.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoController(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        /// <summary>
        /// Cria um novo Pedido
        /// </summary>
        /// <param name="pedidoDto">Dados do novo pedido</param>
        /// <returns>Pedido criado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] PedidoDto pedidoDto)
        {
            var adapterPedido = new AdapterPedidoController( _pedidoRepository);
            var pedidoId = await adapterPedido.Create(pedidoDto.ToDomainModel());

            return new CreatedResult(string.Empty, new {pedidoId});
        }
    }
}