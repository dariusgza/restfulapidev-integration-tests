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
    }
}