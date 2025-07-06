using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IntegrationTests.Framework
{
    public class ObjectsClient
    {
        private readonly HttpClient _client;

        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        internal ObjectsClient(HttpClient client)
        {
            ArgumentNullException.ThrowIfNull(client);
            _client = client;
        }
        internal Task<T?> GetAsync<T>(Uri endpoint)
        {
            ArgumentNullException.ThrowIfNull(endpoint);
            return SendRequestAsync<T>(() => _client.GetAsync(endpoint));
        }

        internal Task<T?> GetObjectByIdAsync<T>(Uri endpoint, string id)
        {
            ArgumentNullException.ThrowIfNull(endpoint);
            ArgumentException.ThrowIfNullOrEmpty(id);
            
            var requestUri = new Uri(endpoint, Uri.EscapeDataString(id));
            return SendRequestAsync<T>(() => _client.GetAsync(requestUri));
        }
        
        internal Task<TResponse?> PostAsync<TRequest, TResponse>(Uri endpoint, TRequest requestBody)
        {
            ArgumentNullException.ThrowIfNull(endpoint);
            ArgumentNullException.ThrowIfNull(requestBody);

            var content = CreateJsonContent(requestBody);
            return SendRequestAsync<TResponse>(() => _client.PostAsync(endpoint, content));
        }

        internal Task<T?> PutAsync<T>(Uri endpoint, T requestBody)
        {
            ArgumentNullException.ThrowIfNull(endpoint);
            ArgumentNullException.ThrowIfNull(requestBody);

            var content = CreateJsonContent(requestBody);
            return SendRequestAsync<T>(() => _client.PutAsync(endpoint, content));
        }

        internal Task<T?> PatchAsync<T>(Uri endpoint, T requestBody)
        {
            ArgumentNullException.ThrowIfNull(endpoint);
            ArgumentNullException.ThrowIfNull(requestBody);

            using var content = CreateJsonContent(requestBody);
            return SendRequestAsync<T>(() => _client.PatchAsync(endpoint, content));
        }

        internal Task<T?> DeleteAsync<T>(Uri endpoint)
        {
            ArgumentNullException.ThrowIfNull(endpoint);
            return SendRequestAsync<T>(() => _client.DeleteAsync(endpoint));
        }

        private static StringContent CreateJsonContent<T>(T data)
        {
            return new StringContent(
                JsonSerializer.Serialize(data, JsonOptions),
                Encoding.UTF8,
                "application/json");
        }

        private static async Task<T?> SendRequestAsync<T>(Func<Task<HttpResponseMessage>> requestFunc)
        {
            using var response = await requestFunc().ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(content, JsonOptions);
        }
    }
}