using DotLanches.Pedidos.Domain.Exceptions;

namespace DotLanches.Pedidos.Domain.ValueObjects;

public class Combo
{
    public IEnumerable<Guid> IdsProduto { get; }
    public decimal PrecoTotal { get; }

    public Combo(IEnumerable<Guid> idsProduto, decimal precoTotal)
    {
        if (idsProduto == null || !idsProduto.Any() || idsProduto.Contains(Guid.Empty))
            throw new DomainValidationException(nameof(idsProduto));

        if (idsProduto.Count() > 4)
            throw new DomainValidationException(nameof(idsProduto));

        if (precoTotal <= 0)
            throw new DomainValidationException(nameof(precoTotal));

        IdsProduto = idsProduto;
        PrecoTotal = precoTotal;
    }
}