using System.Text.Json.Serialization;

namespace IntegrationTests.Models
{
    internal class ObjectsAttributes
    {

        [JsonPropertyName("color")]
        public string? Color { get; set; }

        [JsonPropertyName("capacity")]
        public string? Capacity { get; set; }

        [JsonPropertyName("price")]
        public int? Price { get; set; }

        [JsonPropertyName("generation")]
        public string? Generation { get; set; }

        [JsonPropertyName("year")]
        public int? Year { get; set; }

        [JsonPropertyName("CPU model")]
        public string? CPUModel { get; set; }

        [JsonPropertyName("Hard disk size")]
        public string? HardDiskSize { get; set; }

        [JsonPropertyName("Strap Colour")]
        public string? StrapColour { get; set; }

        [JsonPropertyName("Case Size")]
        public string? CaseSize { get; set; }

        [JsonPropertyName("Description")]
        public string? Description { get; set; }

        [JsonPropertyName("Screen Size")]
        public string? ScreenSize { get; set; }
    }
}