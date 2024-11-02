using DotLanches.Pedidos.Domain.Exceptions;

namespace DotLanches.Pedidos.Domain.ValueObjects;

public class Combo
{
    public Guid IdProduto { get; }
    public decimal Preco { get; }

    public Combo(Guid idProduto, decimal preco)
    {
        if (idProduto == Guid.Empty)
            throw new DomainValidationException(nameof(idProduto));
        
        if (preco <= 0)
            throw new DomainValidationException(nameof(preco));

        IdProduto = idProduto;
        Preco = preco;
    }
}
