#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace DotLanches.Domain.Entities
{
    public enum EStatus
    {
        Cancelado, 
        Confirmado,
        Recebido,
        EmPreparo,
        Pronto,
        Finalizado,
    }
}