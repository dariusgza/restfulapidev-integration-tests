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

            var response = await client.GetObjectByIdAsync<ObjectsResponse>(Endpoints.Objects, id).ConfigureAwait(false);
            return response ?? throw new InvalidOperationException("The API response was null.");
        }        
        public static async Task<ObjectsResponse> PostObjectAsync(ObjectsClient client, ObjectsRequest requestBody)
        {
            ArgumentNullException.ThrowIfNull(requestBody);

            var uri = Endpoints.AddObject(requestBody);
            var response = await client.PostAsync<ObjectsRequest, ObjectsResponse>(uri, requestBody).ConfigureAwait(false);
            return response is null ? throw new InvalidOperationException("The API response was null.") : response;
        }
    }
}