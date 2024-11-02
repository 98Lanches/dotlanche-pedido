using DotLanches.Pedido.Api.Dtos;
using DotLanches.Pedido.Api.Mappers;
using DotLanches.Pedido.Controllers;
using DotLanches.Pedido.Domain.Entities;
using DotLanches.Pedido.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace DotLanches.Pedido.Api.Controllers
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

        /// <summary>
        /// Atualiza o status de um pedido existente
        /// </summary>
        /// <param name="idPedido">ID do pedido a ser atualizado</param>
        /// <param name="statusDto">ID do novo status do pedido</param>
        /// <returns>Pedido atualizado</returns>
        [HttpPut("{idPedido}")]
        [ProducesResponseType(typeof(Pedido), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatus([FromRoute] Guid idPedido, [Required][FromQuery] StatusDto statusDto)
        {
            var adapterPedido = new AdapterPedidoController(_pedidoRepository);
            var updatedPedido = await adapterPedido.UpdateStatus(idPedido, statusDto.Status);
            return Ok(updatedPedido);
        }
    }
}