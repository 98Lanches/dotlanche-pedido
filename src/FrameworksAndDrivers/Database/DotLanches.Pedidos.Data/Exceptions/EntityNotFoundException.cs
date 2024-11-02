namespace DotLanches.Pedidos.DataMongo.Exceptions
{
    public class EntityNotFoundException(string? message = null) : Exception(message ?? "Entity not found!")
    {
    }
}
