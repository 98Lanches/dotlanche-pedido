using DotLanches.Pedidos.Api.Dtos;
using DotLanches.Pedidos.Domain.Entities;

namespace DotLanches.Pedidos.Api.Mappers
{
    public static class PedidoMapper
    {
        public static Pedido ToDomainModel(this PedidoDto pedidoDto)
        {
            var domainModel = new Pedido(DateTime.UtcNow,
                                         pedidoDto.ClienteCpf,
                                         pedidoDto.Combos.Select(c => c.ToDomainModel()).ToList());

            return domainModel;
        }
    }
}
