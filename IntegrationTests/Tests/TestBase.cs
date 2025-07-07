using FluentAssertions;
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

        #region Assertion Helpers

        protected static void AssertSuccessResponse(ObjectsResponse response)
        {
            ArgumentNullException.ThrowIfNull(response);
            response.Should().NotBeNull();
            response.Should().BeOfType<ObjectsResponse>();
            response.Status.Should().Be("success");
        }

        protected static void AssertResponseHasObjects(ObjectsResponse response)
        {
            ArgumentNullException.ThrowIfNull(response);
            AssertSuccessResponse(response);
            response.Response.Should().NotBeNull();
            response.Response.Should().NotBeEmpty();
        }

        protected static void AssertObjectIdMatches(ObjectsResponse response, string expectedId)
        {
            ArgumentNullException.ThrowIfNull(response);
            ArgumentException.ThrowIfNullOrEmpty(expectedId);
            AssertResponseHasObjects(response);
            response.Response[0].Id.Should().Be(expectedId);
        }

        protected static void AssertObjectNameMatches(ObjectsResponse response, string expectedName)
        {
            ArgumentNullException.ThrowIfNull(response);
            ArgumentException.ThrowIfNullOrEmpty(expectedName);
            AssertResponseHasObjects(response);
            response.Response[0].Name.Should().Be(expectedName);
        }

        protected static void AssertObjectMatches(ObjectsResponse response, string expectedId, string expectedName)
        {
            ArgumentNullException.ThrowIfNull(response);
            ArgumentException.ThrowIfNullOrEmpty(expectedId);
            ArgumentException.ThrowIfNullOrEmpty(expectedName);
            AssertResponseHasObjects(response);
            response.Response[0].Id.Should().Be(expectedId);
            response.Response[0].Name.Should().Be(expectedName);
        }

        #endregion
    }
}