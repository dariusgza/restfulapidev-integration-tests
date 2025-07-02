using IntegrationTests.Framework;
using NUnit.Framework;
using System.Net.Http;

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