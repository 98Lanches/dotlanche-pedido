﻿#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace DotLanches.Pedidos.Api.Dtos
{
    public class ComboDto
    {
        public Guid? ProdutoId { get; set; }
        public decimal? Preco { get; set; }
    }
}
