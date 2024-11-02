using DotLanches.Pedidos.Api.Dtos;
using DotLanches.Pedidos.Domain.ValueObjects;

namespace DotLanches.Pedidos.Api.Mappers
{
    public static class ComboMapper
    {
        public static Combo ToDomainModel(this ComboDto comboDto)
        {
            if (comboDto.ProdutoId == null || comboDto.Preco == null)
                throw new ArgumentNullException("ProdutoId and Preco can`t be null");

            return new Combo(comboDto.ProdutoId.Value, comboDto.Preco.Value);
        }
    }
}
