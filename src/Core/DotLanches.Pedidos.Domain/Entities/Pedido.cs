using DotLanches.Pedidos.Domain.Enums;
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
    public Pagamento Pagamento { get; set; }

    public Pedido(DateTime createdAt, string? clienteCpf, IEnumerable<Combo> combos, TipoPagamento tipoPagamento)
    {
        Id = Guid.NewGuid();
        CreatedAt = createdAt;
        ClienteCpf = clienteCpf;
        Combos = combos ?? throw new ArgumentNullException(nameof(combos));
        Pagamento = new Pagamento()
        {
            Amount = TotalPrice,
            Type = tipoPagamento
        };

        ValidateEntity();
    }

    private void ValidateEntity()
    {
        if (!Combos.Any())
            throw new DomainValidationException("O pedido deve conter pelo menos um combo");
    }

    private decimal CalculateTotalPrice()
    {
        return Combos.Sum(c => c.PrecoTotal);
    }
}