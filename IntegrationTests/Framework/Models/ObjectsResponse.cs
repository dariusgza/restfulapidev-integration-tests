using System.Text.Json.Serialization;

namespace IntegrationTests.Models
{
    internal class ObjectsResponse
    {
        [JsonPropertyName("")]
        public required List<Objects> Response { get; set; }
    }
}