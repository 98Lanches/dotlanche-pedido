using DotLanches.Pedido.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace DotLanches.Pedido.Api.Dtos
{
    public class StatusDto
    {
        [Required]
        public EStatus Status { get; set; }
    }
}
