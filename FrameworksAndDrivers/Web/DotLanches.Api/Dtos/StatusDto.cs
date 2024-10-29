using DotLanches.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace DotLanches.Api.Dtos
{
    public class StatusDto
    {
        [Required]
        public EStatus Status { get; set; }
    }
}
