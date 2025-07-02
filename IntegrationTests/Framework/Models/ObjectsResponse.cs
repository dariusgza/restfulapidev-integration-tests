using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IntegrationTests.Models
{
    internal class ObjectsResponse
    {
    public required List<Objects> Response { get; set; }
    public required string Status { get; set; }

    }
}