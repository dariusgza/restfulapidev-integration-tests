using FluentAssertions;
using IntegrationTests.Framework.Services;
using IntegrationTests.Models;
using NUnit.Framework;
using System.Threading.Tasks;

namespace IntegrationTests.Tests
{
    [TestFixture]
    public class ObjectsTests : TestBase
    {
        [Test]
        public async Task GetObjects_ShouldReturnObjects()
        {
            var response = await ObjectsService.GetObjectsAsync(Client).ConfigureAwait(false);
            response.Should().NotBeNull();
            response.Response.Should().NotBeEmpty();
            response.Status.Should().Be("success");
        }

        [Test]
        public async Task GetObjectById_ShouldReturnObject()
        {
            // First, create a test object to get a valid ID
            var createRequest = new ObjectsRequest
            {
                Name = "Test Object For GetById",
                Data = "Sample Data"
            };
            var createResponse = await ObjectsService.PostObjectAsync(Client, createRequest).ConfigureAwait(false);
            createResponse.Should().NotBeNull();
            createResponse.Response.Should().NotBeEmpty();
            var id = createResponse.Response.First().Id; // Assumes 'Id' property exists on Objects

            // Now, get the object by its ID
            var response = await ObjectsService.GetObjectByIdAsync(Client, id).ConfigureAwait(false);
            response.Should().NotBeNull();
            response.Response.Should().NotBeEmpty();
            response.Status.Should().Be("success");
        }

        [Test]
        public async Task PostObject_ShouldCreateObject()
        {
            var requestBody = new ObjectsRequest
            {
                Name = "Test Object",
                Data = "Sample Data"
            };

            var response = await ObjectsService.PostObjectAsync(Client, requestBody).ConfigureAwait(false);
            response.Should().NotBeNull();
            response.Response.Should().NotBeEmpty();
            response.Status.Should().Be("success");
        }
    }
}