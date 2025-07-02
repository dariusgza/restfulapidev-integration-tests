namespace IntegrationTests.Framework
{
    internal static class HttpClientFactory
    {
        public static HttpClient Create(bool authenticated = false)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://api.restful-api.dev/objects/")
            };

            return client;
        }
    }
}