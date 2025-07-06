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
        public async Task GivenValidRequest_WhenGetObjects_ThenReturnsObjects()
        {
            // Act
            var response = await ObjectsService.GetObjectsAsync(Client).ConfigureAwait(false);

            // Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<ObjectsResponse>();
            response.Response.Should().NotBeNullOrEmpty();
            response.Status.Should().Be("success");
        }
    }
}