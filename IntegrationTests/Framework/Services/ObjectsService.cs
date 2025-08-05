using System.Collections.ObjectModel;
using IntegrationTests.Models;

namespace IntegrationTests.Framework.Services
{
    internal static class ObjectsService
    {
        public static async Task<ObjectsResponse> GetObjectsAsync(ObjectsClient client)
        {
            ArgumentNullException.ThrowIfNull(client);

            var objects = await client.GetAsync<List<Objects>>(Endpoints.Objects).ConfigureAwait(false);
            return new ObjectsResponse { 
                Status = "success",
                Response = new Collection<Objects>(objects ?? new List<Objects>())
            };
        }

        public static async Task<ObjectsResponse> GetObjectByIdAsync(ObjectsClient client, string id)
        {
            ArgumentNullException.ThrowIfNull(client);
            ArgumentException.ThrowIfNullOrEmpty(id);

            var obj = await client.GetObjectByIdAsync<Objects>(Endpoints.Objects, id).ConfigureAwait(false)
                ?? throw new InvalidOperationException("The API response was null.");

            return new ObjectsResponse
            {
                Status = "success",
                Response = new Collection<Objects>(new List<Objects> { obj })
            };
        }        
        public static async Task<ObjectsResponse> PostObjectAsync(ObjectsClient client, ObjectsRequest requestBody)
        {
            ArgumentNullException.ThrowIfNull(client);
            ArgumentNullException.ThrowIfNull(requestBody);
            ArgumentNullException.ThrowIfNull(requestBody.Data);
            if (string.IsNullOrWhiteSpace(requestBody.Name))
                throw new ArgumentException("Name cannot be empty.", nameof(requestBody));

            var obj = await client.PostAsync<ObjectsRequest, Objects>(Endpoints.Objects, requestBody).ConfigureAwait(false)
                ?? throw new InvalidOperationException("The API response was null.");

            return new ObjectsResponse
            {
                Status = "success",
                Response = new Collection<Objects>(new List<Objects> { obj })
            };
        }

        public static async Task<ObjectsResponse> PutObjectAsync(ObjectsClient client, string id, ObjectsRequest requestBody)
        {
            ArgumentNullException.ThrowIfNull(client);
            ArgumentException.ThrowIfNullOrEmpty(id);
            ArgumentNullException.ThrowIfNull(requestBody);
            ArgumentNullException.ThrowIfNull(requestBody.Data);
            if (string.IsNullOrWhiteSpace(requestBody.Name))
                throw new ArgumentException("Name cannot be empty.", nameof(requestBody));

            var requestUri = new Uri($"{Endpoints.Objects}/{id}", UriKind.Relative);
            var obj = await client.PutAsync<ObjectsRequest, Objects>(requestUri, requestBody).ConfigureAwait(false)
                ?? throw new InvalidOperationException("The API response was null.");

            return new ObjectsResponse
            {
                Status = "success",
                Response = new Collection<Objects>(new List<Objects> { obj })
            };
        }

        public static async Task<ObjectsResponse> PatchObjectAsync(ObjectsClient client, string id, ObjectsRequest requestBody)
        {
            ArgumentNullException.ThrowIfNull(client);
            ArgumentException.ThrowIfNullOrEmpty(id);
            ArgumentNullException.ThrowIfNull(requestBody);
            if (requestBody.Data == null && string.IsNullOrWhiteSpace(requestBody.Name))
                throw new ArgumentException("Request must contain non-empty Name and Data.", nameof(requestBody));
            if (string.IsNullOrWhiteSpace(requestBody.Name))
                throw new ArgumentException("Name cannot be empty.", nameof(requestBody));

            var requestUri = new Uri($"{Endpoints.Objects}/{id}", UriKind.Relative);
            var obj = await client.PatchAsync<ObjectsRequest, Objects>(requestUri, requestBody).ConfigureAwait(false)
                ?? throw new InvalidOperationException("The API response was null.");

            return new ObjectsResponse
            {
                Status = "success",
                Response = new Collection<Objects>(new List<Objects> { obj })
            };
        }

        public static async Task<ObjectsResponse> DeleteObjectAsync(ObjectsClient client, string id)
        {
            ArgumentNullException.ThrowIfNull(client);
            ArgumentException.ThrowIfNullOrWhiteSpace(id);

            var requestUri = new Uri($"{Endpoints.Objects}/{id}", UriKind.Relative);
            var deleteResponse = await client.DeleteAsync<DeleteResponse>(requestUri).ConfigureAwait(false)
                ?? throw new InvalidOperationException("The API response was null.");

            var deletedObject = new Objects
            {
                Id = id,
                Name = "Deleted",
                Data = null
            };

            return new ObjectsResponse
            {
                Status = "success",
                Response = new Collection<Objects>(new List<Objects> { deletedObject })
            };
        }
        
    }
}