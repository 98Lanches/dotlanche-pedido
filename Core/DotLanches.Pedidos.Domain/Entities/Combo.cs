#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
using DotLanches.Pedidos.Domain.Exceptions;

namespace DotLanches.Pedidos.Domain.Entities;

public class Combo
{
    public Guid Id { get; set; }
    public Guid IdLanche { get; set; }
    public Guid IdAcompanhamento { get; set; }
    public Guid IdBebida { get; set; }
    public Guid IdSobremesa { get; set; }
    public decimal PriceLanche { get; set; }
    public decimal PriceAcompanhamento { get; set; }
    public decimal PriceBebida { get; set; }
    public decimal PriceSobremesa { get; set; }

    public Combo(
        Guid lanche,
        Guid acompanhamento,
        Guid bebida,
        Guid sobremesa,
        decimal priceLanche,
        decimal priceAcompanhamento,
        decimal priceBebida,
        decimal priceSobremesa)
    {
        Id = Guid.NewGuid();
        IdLanche = lanche;
        IdAcompanhamento = acompanhamento;
        IdBebida = bebida;
        IdSobremesa = sobremesa;
        PriceLanche = priceLanche;
        PriceAcompanhamento = priceAcompanhamento;
        PriceBebida = priceBebida;
        PriceSobremesa = priceSobremesa;

        ValidateEntity();
    }

    private void ValidateEntity()
    {
        if (IdLanche == Guid.Empty)
            throw new DomainValidationException(nameof(IdLanche));
        
        if (IdAcompanhamento == Guid.Empty)
            throw new DomainValidationException(nameof(IdAcompanhamento));
        
        if (IdBebida == Guid.Empty)
            throw new DomainValidationException(nameof(IdBebida));
        
        if (IdSobremesa == Guid.Empty)
            throw new DomainValidationException(nameof(IdSobremesa));
        
        if (PriceLanche <= 0)
            throw new DomainValidationException(nameof(PriceLanche));
        
        if (PriceAcompanhamento <= 0)
            throw new DomainValidationException(nameof(PriceAcompanhamento));
        
        if (PriceBebida <= 0)
            throw new DomainValidationException(nameof(PriceBebida));
        
        if (PriceSobremesa <= 0)
            throw new DomainValidationException(nameof(PriceSobremesa));
    }

    public decimal CalculatePrice()
    {
        decimal price = 0;
        price += PriceLanche;
        price += PriceAcompanhamento;
        price += PriceBebida;
        price += PriceSobremesa;

        if (price <= 0)
            throw new DomainValidationException(nameof(price));

        return price; 
    }
}