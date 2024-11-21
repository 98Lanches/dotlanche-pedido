using DotLanches.Pedidos.Api.Dtos;
using DotLanches.Pedidos.Domain.ValueObjects;

namespace DotLanches.Pedidos.Api.Mappers
{
    public static class ComboMapper
    {
        public static Combo ToDomainModel(this ComboDto comboDto)
        {
            if (comboDto.IdsProduto == null || !comboDto.IdsProduto.Any())
                throw new ArgumentNullException("IdsProduto cannot be null or empty");

            if (comboDto.PrecoTotal <= 0)
                throw new ArgumentOutOfRangeException("PrecoTotal must be greater than zero");

            return new Combo(comboDto.IdsProduto, comboDto.PrecoTotal);
        }
    }
}