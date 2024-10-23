namespace DotLanches.Domain.Entities
{
    public record QueueKey
    {
        public QueueKey(int value, DateTime creationDate)
        {
            Value = value;
            CreationDate = creationDate;
        }

        public int Value { get; init; }
        public DateTime CreationDate { get; init; }
    }
}