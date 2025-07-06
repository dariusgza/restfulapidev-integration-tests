using System.Text.Json;
using System.Text.Json.Serialization;

namespace IntegrationTests.Models
{
    public class StringOrNumberConverter : JsonConverter<string>
    {
        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            JsonTokenType tokenType = reader.TokenType;
            
            return tokenType switch
            {
                JsonTokenType.String => reader.GetString(),
                JsonTokenType.Number => reader.GetDecimal().ToString(System.Globalization.CultureInfo.InvariantCulture),
                JsonTokenType.Null => null,
                _ => throw new JsonException($"Unexpected token type: {tokenType}")
            };
        }

        public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options)
        {
            ArgumentNullException.ThrowIfNull(writer);
            if (value is null)
                writer.WriteNullValue();
            else
                writer.WriteStringValue(value);
        }
    }
}
