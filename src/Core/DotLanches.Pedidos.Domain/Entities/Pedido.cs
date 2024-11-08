using DotLanches.Pedidos.Domain.Exceptions;
using DotLanches.Pedidos.Domain.ValueObjects;

namespace DotLanches.Pedidos.Domain.Entities;

public class Pedido
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string? ClienteCpf { get; private set; }
    public decimal TotalPrice => CalculateTotalPrice();
    public IEnumerable<Combo> Combos { get; private set; }

    private Pedido() { }

    public Pedido(DateTime createdAt, string? clienteCpf, IEnumerable<Combo> combos)
    {
        Id = Guid.NewGuid();
        CreatedAt = createdAt;
        ClienteCpf = clienteCpf;
        Combos = combos ?? throw new ArgumentNullException(nameof(combos));

        ValidateEntity();
    }

    private void ValidateEntity()
    {
        if (!Combos.Any())
            throw new DomainValidationException("O pedido deve conter pelo menos um combo.");
        
        if (TotalPrice <= 0)
            throw new DomainValidationException("O preço total do pedido deve ser maior que zero.");
    }

    private decimal CalculateTotalPrice()
    {
        return Combos.Sum(c => c.Preco);
    }
}
