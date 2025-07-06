using IntegrationTests.Framework;

namespace IntegrationTests.Tests
{
    public abstract class TestBase
    {
        public ObjectsClient Client { get; private set; }

        [SetUp]
        public void Setup()
        {
            var httpClient = HttpClientFactory.Create();
            Client = new ObjectsClient(httpClient);
        }
    }
}