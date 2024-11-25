namespace DotLanches.Pedidos.Presenters.Dtos
{
    public class ComboDto
    {
        public required IEnumerable<Guid> IdsProduto { get; set; } = [];
        public required decimal PrecoTotal { get; set; }
    }
}
