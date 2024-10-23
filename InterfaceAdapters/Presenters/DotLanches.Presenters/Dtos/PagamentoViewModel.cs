using System.Text.Json.Serialization;

namespace DotLanches.Presenters.Dtos
{
    public class PagamentoViewModel
    {
        public bool? IsAccepted { get; set; }

        public DateTime? RegisteredAt { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? QueueKey { get; set; }
    }
}
