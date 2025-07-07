using System.Text.Json.Serialization;

namespace IntegrationTests.Models
{
    public class ObjectsRequest
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }
        
        [JsonPropertyName("data")]
        public required ObjectsAttributes Data { get; set; }
    }
}
