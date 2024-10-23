#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
using DotLanches.Domain.Exceptions;

namespace DotLanches.Domain.Entities;

public class Pedido
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? ClienteCpf { get; set; }
    public EStatus Status { get; set; }
    public decimal TotalPrice { get; set; }
    public Guid IdCombo { get; set; }
    public int QueueKey { get; set; }
    public DateTime AddedToQueueAt { get; set; }
    public Guid IdPagamento { get; set; }

    private Pedido() { }

    public Pedido(DateTime createdAt, string? clienteCpf, Guid combo)
    {
        Id = Guid.NewGuid();
        CreatedAt = createdAt;
        ClienteCpf = clienteCpf;
        IdCombo = combo;
        Status = EStatus.Confirmado;

        ValidateEntity();
    }

    private void ValidateEntity()
    {
        if (IdCombo == Guid.Empty)
            throw new DomainValidationException(nameof(IdCombo));        
    }

    public void Cancel()
    {
        this.Status = EStatus.Cancelado;
    }

}