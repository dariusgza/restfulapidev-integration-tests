using IntegrationTests.Framework;
using IntegrationTests.Framework.Services;
using IntegrationTests.Models;

namespace IntegrationTests.Tests
{
    public abstract class TestBase
    {
        private readonly List<string> _createdObjectIds = new();
        public ObjectsClient Client { get; private set; }

        [SetUp]
        public void Setup()
        {
            var httpClient = HttpClientFactory.Create();
            Client = new ObjectsClient(httpClient);
        }

        [TearDown]
        public async Task Cleanup()
        {
            foreach (var objectId in _createdObjectIds.ToList())
            {
                try
                {
                    await ObjectsService.DeleteObjectAsync(Client, objectId).ConfigureAwait(false);
                    _createdObjectIds.Remove(objectId);
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("404", StringComparison.OrdinalIgnoreCase))
                {
                    // Object already deleted, remove from tracking
                    _createdObjectIds.Remove(objectId);
                }
                catch (HttpRequestException)
                {
                    // Log or handle other HTTP exceptions if needed
                }
            }
        }

        protected void TrackObject(Objects obj)
        {
            ArgumentNullException.ThrowIfNull(obj);
            _createdObjectIds.Add(obj.Id);
        }
    }
}