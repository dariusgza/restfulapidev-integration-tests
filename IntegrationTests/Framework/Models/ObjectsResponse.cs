using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace IntegrationTests.Models
{
    public class ObjectsResponse
    {
        [JsonPropertyName("status")]
        public required string Status { get; set; }

        [JsonPropertyName("response")]
        public required Collection<Objects> Response { get; init; }

    }
}