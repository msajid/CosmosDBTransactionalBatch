using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TestTransactionalBatchConsole
{
    public class Metadata
    {
        public string id { get; set; } = "0";
        public string AccountNumber { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Status AccountStatus { get; set; }
        public string LastSequence { get; set; }
        public string DocumentType { get; set; } = nameof(Metadata);
    }

    public enum Status
    {
        Active,
        InActive
    }
}
