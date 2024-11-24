using DotLanches.Pedidos.Domain.Entities;
using DotLanches.Pedidos.Presenters.Dtos;

namespace DotLanches.Pedidos.Api.Mappers
{
    public static class PedidoMapper
    {
        public static Pedido ToDomainModel(this CreatePedidoRequest pedidoDto)
        {
            var domainModel = new Pedido(DateTime.UtcNow,
                                         pedidoDto.ClienteCpf,
                                         pedidoDto.Combos.Select(c => c.ToDomainModel()).ToList(),
                                         pedidoDto.TipoPagamento);

            return domainModel;
        }

        public static CreatePedidoResponse ToResponse(this Pedido pedido)
        {
            var response = new CreatePedidoResponse()
            {
                PedidoId = pedido.Id,
                PagamentoInformation = pedido.Pagamento.PagamentoData ?? null,
            };

            return response;
        }
    }
}