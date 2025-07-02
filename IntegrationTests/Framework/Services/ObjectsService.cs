using IntegrationTests.Models;

namespace IntegrationTests.Framework.Services
{
    internal static class ObjectsService
    {
        public static async Task<ObjectsResponse> GetObjectsAsync(ObjectsClient client)
        {
            var response = await client.GetObjectsAsync<ObjectsResponse>(Endpoints.Objects).ConfigureAwait(false);
            return response!;
        }

        public static async Task<ObjectsResponse> GetObjectByIdAsync(ObjectsClient client, string id)
        {
            ArgumentNullException.ThrowIfNull(client);
            ArgumentException.ThrowIfNullOrEmpty(id);

            var response = await client.GetObjectByIdAsync<ObjectsResponse>(Endpoints.Objects, id).ConfigureAwait(false);
            return response!;
        }        
        public static async Task<ObjectsResponse> PostObjectAsync(ObjectsClient client, ObjectsRequest requestBody)
        {
            ArgumentNullException.ThrowIfNull(requestBody);

            var uri = Endpoints.AddObject(requestBody);
            var response = await client.PostObjectAsync<ObjectsRequest, ObjectsResponse>(uri, requestBody).ConfigureAwait(false);
            return response is null ? throw new InvalidOperationException("The API response was null.") : response;
        }
    }
}