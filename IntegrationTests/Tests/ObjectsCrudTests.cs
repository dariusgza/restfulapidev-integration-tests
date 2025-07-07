using FluentAssertions;
using IntegrationTests.Framework.Services;
using IntegrationTests.Models;
using NUnit.Framework;
using System.Threading.Tasks;
using IntegrationTests.TestBuilders;

namespace IntegrationTests.Tests
{
    [TestFixture]
    [Category("Integration")]
    public class ObjectsCrudTests : TestBase
    {
        #region GET Operations

        [Test]
        [Category("Read")]
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

        [Test]
        [Category("Read")]
        public async Task GivenValidId_WhenGetObjectById_ThenReturnsObject()
        {
            // Arrange
            var createdObject = await CreateTestObject("get-by-id-test").ConfigureAwait(false);

            // Act
            var response = await ObjectsService.GetObjectByIdAsync(Client, createdObject.Id).ConfigureAwait(false);

            // Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<ObjectsResponse>();
            response.Status.Should().Be("success");
            response.Response.Should().NotBeNull();
            response.Response[0].Id.Should().Be(createdObject.Id);
        }

        [Test]
        [Category("Read")]
        [Category("Error")]
        public async Task GivenNonExistentId_WhenGetObjectById_ThenThrowsHttpRequestException()
        {
            // Arrange
            const string nonExistentId = "non-existent-id";

            // Act & Assert
            var action = () => ObjectsService.GetObjectByIdAsync(Client, nonExistentId);
            await action.Should()
                .ThrowAsync<HttpRequestException>()
                .WithMessage("*404*")
                .ConfigureAwait(false);
        }

        [Test]
        [Category("Read")]
        [Category("Validation")]
        public async Task GivenInvalidId_WhenGetObjectById_ThenThrowsArgumentException()
        {
            // Arrange
            const string invalidId = "";

            // Act & Assert
            var action = () => ObjectsService.GetObjectByIdAsync(Client, invalidId);
            await action.Should()
                .ThrowAsync<ArgumentException>()
                .WithMessage("*ID*")
                .ConfigureAwait(false);
        }

        #endregion

        #region POST Operations

        [Test]
        [Category("Create")]
        public async Task GivenValidRequest_WhenPostObject_ThenReturnsCreatedObject()
        {
            // Arrange
            var request = ObjectsRequestBuilder.Create()
                .WithName("test-object")
                .WithData(builder => builder.WithDescription("Test object for integration test"))
                .Build();

            // Act
            var response = await ObjectsService.PostObjectAsync(Client, request).ConfigureAwait(false);

            // Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<ObjectsResponse>();
            response.Status.Should().Be("success");
            response.Response.Should().NotBeNull();
            response.Response[0].Name.Should().Be(request.Name);
        }

        [Test]
        [Category("Create")]
        [Category("Validation")]
        public async Task GivenEmptyName_WhenPostObject_ThenThrowsArgumentException()
        {
            // Arrange
            var request = ObjectsRequestBuilder.Create()
                .WithName("")
                .WithData(builder => builder.WithDescription("Test object"))
                .Build();

            // Act & Assert
            var action = () => ObjectsService.PostObjectAsync(Client, request);
            await action.Should()
                .ThrowAsync<ArgumentException>()
                .WithMessage("*Name*")
                .ConfigureAwait(false);
        }

        [Test]
        [Category("Create")]
        [Category("Validation")]
        public async Task GivenNullData_WhenPostObject_ThenThrowsArgumentNullException()
        {
            // Arrange
            var request = ObjectsRequestBuilder.Create()
                .WithName("test-object")
                .WithData((ObjectsAttributes)null!)
                .Build();

            // Act & Assert
            var action = () => ObjectsService.PostObjectAsync(Client, request);
            await action.Should()
                .ThrowAsync<ArgumentNullException>()
                .ConfigureAwait(false);
        }

        #endregion

        #region PUT Operations

        [Test]
        [Category("Update")]
        public async Task GivenValidRequest_WhenPutObject_ThenReturnsUpdatedObject()
        {
            // Arrange
            var createdObject = await CreateTestObject("put-test").ConfigureAwait(false);

            var updateRequest = ObjectsRequestBuilder.Create()
                .WithName("updated-object")
                .WithData(builder => builder.WithDescription("Updated test object"))
                .Build();

            // Act
            var response = await ObjectsService.PutObjectAsync(Client, createdObject.Id, updateRequest).ConfigureAwait(false);

            // Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<ObjectsResponse>();
            response.Status.Should().Be("success");
            response.Response.Should().NotBeNull();
            response.Response[0].Id.Should().Be(createdObject.Id);
            response.Response[0].Name.Should().Be(updateRequest.Name);
        }

        [Test]
        [Category("Update")]
        [Category("Error")]
        public async Task GivenNonExistentId_WhenPutObject_ThenThrowsHttpRequestException()
        {
            // Arrange
            const string nonExistentId = "non-existent-id";
            var request = ObjectsRequestBuilder.Create()
                .WithName("updated-object")
                .WithData(builder => builder.WithDescription("Updated test object"))
                .Build();

            // Act & Assert
            var action = () => ObjectsService.PutObjectAsync(Client, nonExistentId, request);
            await action.Should()
                .ThrowAsync<HttpRequestException>()
                .WithMessage("*404*")
                .ConfigureAwait(false);
        }

        [Test]
        [Category("Update")]
        [Category("Validation")]
        public async Task GivenNullRequest_WhenPutObject_ThenThrowsArgumentNullException()
        {
            // Arrange
            var createdObject = await CreateTestObject("put-null-test").ConfigureAwait(false);
            ObjectsRequest? request = null;

            // Act & Assert
            var action = () => ObjectsService.PutObjectAsync(Client, createdObject.Id, request!);
            await action.Should()
                .ThrowAsync<ArgumentNullException>()
                .ConfigureAwait(false);
        }

        #endregion

        #region PATCH Operations

        [Test]
        [Category("Update")]
        public async Task GivenValidRequest_WhenPatchObject_ThenReturnsUpdatedObject()
        {
            // Arrange
            var createdObject = await CreateTestObject("patch-test").ConfigureAwait(false);

            var patchRequest = ObjectsRequestBuilder.Create()
                .WithName("patched-object")
                .WithData(builder => builder.WithDescription("Patched test object"))
                .Build();

            // Act
            var response = await ObjectsService.PatchObjectAsync(Client, createdObject.Id, patchRequest).ConfigureAwait(false);

            // Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<ObjectsResponse>();
            response.Status.Should().Be("success");
            response.Response.Should().NotBeNull();
            response.Response[0].Id.Should().Be(createdObject.Id);
            response.Response[0].Name.Should().Be(patchRequest.Name);
        }

        [Test]
        [Category("Update")]
        [Category("Error")]
        public async Task GivenNonExistentId_WhenPatchObject_ThenThrowsHttpRequestException()
        {
            // Arrange
            const string nonExistentId = "non-existent-id";
            var request = ObjectsRequestBuilder.Create()
                .WithName("patched-object")
                .WithData(builder => builder.WithDescription("Patched test object"))
                .Build();

            // Act & Assert
            var action = () => ObjectsService.PatchObjectAsync(Client, nonExistentId, request);
            await action.Should()
                .ThrowAsync<HttpRequestException>()
                .WithMessage("*404*")
                .ConfigureAwait(false);
        }

        [Test]
        [Category("Update")]
        [Category("Validation")]
        public async Task GivenEmptyRequest_WhenPatchObject_ThenThrowsArgumentException()
        {
            // Arrange
            var createdObject = await CreateTestObject("patch-empty-test").ConfigureAwait(false);

            var emptyRequest = ObjectsRequestBuilder.Create()
                .WithName("")
                .WithData(builder => builder.WithDescription(""))
                .Build();

            // Act & Assert
            var action = () => ObjectsService.PatchObjectAsync(Client, createdObject.Id, emptyRequest);
            await action.Should()
                .ThrowAsync<ArgumentException>()
                .WithMessage("*Name*")
                .ConfigureAwait(false);
        }

        #endregion

        #region DELETE Operations

        [Test]
        [Category("Delete")]
        public async Task GivenValidId_WhenDeleteObject_ThenReturnsSuccess()
        {
            // Arrange
            var createdObject = await CreateTestObject("delete-test").ConfigureAwait(false);

            // Act
            var response = await ObjectsService.DeleteObjectAsync(Client, createdObject.Id).ConfigureAwait(false);

            // Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<ObjectsResponse>();
            response.Status.Should().Be("success");
            response.Response.Should().NotBeNull();
            response.Response[0].Id.Should().Be(createdObject.Id);
        }

        [Test]
        [Category("Delete")]
        [Category("Error")]
        public async Task GivenNonExistentId_WhenDeleteObject_ThenThrowsHttpRequestException()
        {
            // Arrange
            const string nonExistentId = "non-existent-id";

            // Act & Assert
            var action = () => ObjectsService.DeleteObjectAsync(Client, nonExistentId);
            await action.Should()
                .ThrowAsync<HttpRequestException>()
                .WithMessage("*404*")
                .ConfigureAwait(false);
        }

        [Test]
        [Category("Delete")]
        [Category("Validation")]
        public async Task GivenEmptyId_WhenDeleteObject_ThenThrowsArgumentException()
        {
            // Arrange
            const string emptyId = "";

            // Act & Assert
            var action = () => ObjectsService.DeleteObjectAsync(Client, emptyId);
            await action.Should()
                .ThrowAsync<ArgumentException>()
                .WithMessage("*ID*")
                .ConfigureAwait(false);
        }

        #endregion

        #region Helper Methods

        private async Task<Objects> CreateTestObject(string namePrefix)
        {
            var request = ObjectsRequestBuilder.Create()
                .WithName($"{namePrefix}-{Guid.NewGuid()}")
                .WithData(builder => builder.WithDescription($"Test object for {namePrefix}"))
                .Build();

            var response = await ObjectsService.PostObjectAsync(Client, request).ConfigureAwait(false);
            var createdObject = response.Response[0];
            TrackObject(createdObject);
            return createdObject;
        }

        #endregion
    }
}
