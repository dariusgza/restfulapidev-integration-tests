using System.Text;
using System.Text.Json;

namespace IntegrationTests.Framework
{
    public class ObjectsClient
    {
        private readonly HttpClient _client;

        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        internal ObjectsClient(HttpClient client)
        {
            _client = client;
        }

        internal async Task<T?> GetObjectsAsync<T>(Uri endpoint)
        {
            var response = await _client.GetAsync(endpoint).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(json, JsonOptions);
        }

        internal async Task<T?> PostObjectAsync<T>(Uri endpoint, T requestBody)
        {
            using var jsonContent = new StringContent(
              JsonSerializer.Serialize(requestBody, JsonOptions),
              Encoding.UTF8,
              "application/json");

            var response = await _client.PostAsync(endpoint, jsonContent).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(responseJson, JsonOptions);
        }

        internal async Task<T?> PutObjectAsync<T>(Uri endpoint, T requestBody)
        {
            using var jsonContent = new StringContent(
              JsonSerializer.Serialize(requestBody, JsonOptions),
              Encoding.UTF8,
              "application/json");

            var response = await _client.PutAsync(endpoint, jsonContent).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(responseJson, JsonOptions);
        }

        internal async Task<T?> PatchObjectAsync<T>(Uri endpoint, T requestBody)
        {
            using var jsonContent = new StringContent(
              JsonSerializer.Serialize(requestBody, JsonOptions),
              Encoding.UTF8,
              "application/json");

            var response = await _client.PatchAsync(endpoint, jsonContent).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(responseJson, JsonOptions);
        }

        internal async Task<T?> DeleteObjectAsync<T>(Uri endpoint)
        {
            var response = await _client.DeleteAsync(endpoint).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(json, JsonOptions);
        }
    }
}