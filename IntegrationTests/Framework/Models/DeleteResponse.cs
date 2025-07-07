using System.Text.Json.Serialization;

namespace IntegrationTests.Models
{
    public class DeleteResponse
    {
        [JsonPropertyName("message")]
        public required string Message { get; set; }
    }
}
