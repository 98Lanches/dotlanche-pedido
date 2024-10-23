namespace DotLanches.UseCases.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException() : base("Object already registered") { }
    }
}
