using System.Text.Json.Serialization;

namespace IntegrationTests.Models
{
    internal class Objects
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("data")]
        public ObjectsAttributes? Data { get; set; }
    }
}