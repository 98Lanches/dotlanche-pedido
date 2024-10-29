using DotLanches.Api.Dtos;
using DotLanches.Domain.Entities;

namespace DotLanches.Api.Mappers
{
    public static class ComboMapper
    {
        public static Combo ToDomainModel(this ComboDto comboDto)
        {
            var domainModel = new Combo(comboDto.LancheId ?? Guid.Empty,
                                        comboDto.AcompanhamentoId ?? Guid.Empty,
                                        comboDto.BebidaId ?? Guid.Empty,
                                        comboDto.SobremesaId ?? Guid.Empty);

            return domainModel;
        }
    }
}
