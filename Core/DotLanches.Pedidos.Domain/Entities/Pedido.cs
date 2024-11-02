#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
using DotLanches.Pedidos.Domain.Exceptions;

namespace DotLanches.Pedidos.Domain.Entities;

public class Pedido
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? ClienteCpf { get; set; }
    public decimal TotalPrice { get; set; }
    public IEnumerable<Combo> Combos { get; set; }

    private Pedido() { }

    public Pedido(DateTime createdAt, string? clienteCpf, IEnumerable<Combo> combos)
    {
        Id = Guid.NewGuid();
        CreatedAt = createdAt;
        ClienteCpf = clienteCpf;
        Combos = combos;

        ValidateEntity();
    }

    private void ValidateEntity()
    {
        if (Combos is null || !Combos.Any())
            throw new DomainValidationException(nameof(Combos));        
    }

    public void CalculateTotalPrice()
    {
        TotalPrice = Combos.Sum(c => c.CalculatePrice());

        if (TotalPrice <= 0)
            throw new DomainValidationException(nameof(TotalPrice));
    }
}