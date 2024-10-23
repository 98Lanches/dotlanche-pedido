#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
using System.ComponentModel.DataAnnotations;

namespace DotLanches.Api.Dtos
{
    public class PedidoDto
    {
        public string? ClienteCpf { get; set; }

        [Required]
        public IEnumerable<ComboDto> Combos { get; set; }
    }
}