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
            
            var requestUri = new Uri(endpoint.ToString().TrimEnd('/') + "/" + Uri.EscapeDataString(id), UriKind.Relative);
            return SendRequestAsync<T>(() => _client.GetAsync(requestUri));
        }
        
        internal Task<TResponse?> PostAsync<TRequest, TResponse>(Uri endpoint, TRequest requestBody)
        {
            ArgumentNullException.ThrowIfNull(endpoint);
            ArgumentNullException.ThrowIfNull(requestBody);

            var content = CreateJsonContent(requestBody);
            return SendRequestAsync<TResponse>(() => _client.PostAsync(endpoint, content));
        }

        internal Task<TResponse?> PutAsync<TRequest, TResponse>(Uri endpoint, TRequest requestBody)
        {
            ArgumentNullException.ThrowIfNull(endpoint);
            ArgumentNullException.ThrowIfNull(requestBody);

            var content = CreateJsonContent(requestBody);
            return SendRequestAsync<TResponse>(() => _client.PutAsync(endpoint, content));
        }

        internal Task<TResponse?> PatchAsync<TRequest, TResponse>(Uri endpoint, TRequest requestBody)
        {
            ArgumentNullException.ThrowIfNull(endpoint);
            ArgumentNullException.ThrowIfNull(requestBody);

            var content = CreateJsonContent(requestBody);
            return SendRequestAsync<TResponse>(() => _client.PatchAsync(endpoint, content));
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
            await using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            return await JsonSerializer.DeserializeAsync<T>(responseStream, JsonOptions).ConfigureAwait(false);
        }
    }
}