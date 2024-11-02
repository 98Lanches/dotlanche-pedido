using DotLanches.Pedido.Api.Dtos;
using DotLanches.Pedido.Domain.Entities;

namespace DotLanches.Pedido.Api.Mappers
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
