using FluentAssertions;
using IntegrationTests.Framework.Services;
using IntegrationTests.Models;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using IntegrationTests.TestBuilders;
using Newtonsoft.Json;

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
            AssertResponseHasObjects(response);
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
            AssertObjectIdMatches(response, createdObject.Id);
        }

        [TestCase("")]
        [Category("Read")]
        [Category("Validation")]
        public async Task GivenInvalidId_WhenGetObjectById_ThenThrowsArgumentException(string invalidId)
        {
            // Arrange & Act
            var act = () => ObjectsService.GetObjectByIdAsync(Client, invalidId);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("*The value cannot be an empty string. (Parameter 'id')")
                .ConfigureAwait(false);
        }

        [TestCase("999999")]
        [TestCase("invalid-id")]
        [Category("Read")]
        [Category("Error")]
        public async Task GivenNonExistentId_WhenGetObjectById_ThenThrowsHttpRequestException(string nonExistentId)
        {
            // Arrange & Act
            Func<Task<ObjectsResponse>> act = () => ObjectsService.GetObjectByIdAsync(Client, nonExistentId);

            // Assert
            await act.Should().ThrowAsync<HttpRequestException>()
                .WithMessage("*Response status code does not indicate success: 404 (Not Found)*")
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
            AssertObjectNameMatches(response, request.Name);
        }

        [TestCase("")]
        [TestCase(" ")]
        [Category("Create")]
        [Category("Validation")]
        public async Task GivenInvalidName_WhenPostObject_ThenThrowsArgumentException(string invalidName)
        {
            // Arrange
            var request = ObjectsRequestBuilder.Create()
                .WithName(invalidName)
                .Build();

            // Act
            var act = () => ObjectsService.PostObjectAsync(Client, request);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("*Name cannot be empty. (Parameter 'requestBody')*")
                .ConfigureAwait(false);
        }

        [Test]
        [Category("Create")]
        [Category("Validation")]
        public async Task GivenNullName_WhenPostObject_ThenThrowsArgumentException()
        {
            // Arrange
            var request = ObjectsRequestBuilder.Create()
                .WithName(null!)
                .Build();

            // Act
            var act = () => ObjectsService.PostObjectAsync(Client, request);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("*Name cannot be empty. (Parameter 'requestBody')*")
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
            var act = () => ObjectsService.PostObjectAsync(Client, request);
            await act.Should()
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

            // Act & Assert
            var act = () => ObjectsService.PutObjectAsync(Client, createdObject.Id, updateRequest);
            await act.Should()
                .NotThrowAsync<HttpRequestException>()
                .ConfigureAwait(false);
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
            var act = () => ObjectsService.PutObjectAsync(Client, nonExistentId, request);
            await act.Should()
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
            var act = () => ObjectsService.PutObjectAsync(Client, createdObject.Id, request!);
            await act.Should()
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
            AssertObjectMatches(response, createdObject.Id, patchRequest.Name);
        }

        [TestCase("999999", "Updated Name")]
        [TestCase("invalid-id", "New Name")]
        [Category("Update")]
        [Category("Error")]
        public async Task GivenNonExistentId_WhenPatchObject_ThenThrowsHttpRequestException(string nonExistentId, string updatedName)
        {
            // Arrange
            var request = ObjectsRequestBuilder.Create()
                .WithName(updatedName)
                .WithData(builder => builder.WithDescription("Updated test object"))
                .Build();

            // Act & Assert
            var act = () => ObjectsService.PatchObjectAsync(Client, nonExistentId, request);
            await act.Should()
                .ThrowAsync<HttpRequestException>()
                .WithMessage("*404*")
                .ConfigureAwait(false);
        }

        [TestCase("", "Valid Name", Description = "Empty Description")]
        [TestCase("Valid Description", "", Description = "Empty Name")]
        [TestCase(" ", " ", Description = "Whitespace Only")]
        [Category("Update")]
        [Category("Validation")]
        public async Task GivenEmptyRequest_WhenPatchObject_ThenThrowsArgumentException(string description, string name)
        {
            // Arrange
            var createdObject = await CreateTestObject("patch-empty-test").ConfigureAwait(false);

            var emptyRequest = ObjectsRequestBuilder.Create()
                .WithName(name)
                .WithData(builder => builder.WithDescription(description))
                .Build();

            // Act & Assert
            var act = () => ObjectsService.PatchObjectAsync(Client, createdObject.Id, emptyRequest);
            await act.Should()
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
            AssertObjectIdMatches(response, createdObject.Id);
        }

        [TestCase("999999", Description = "Numeric non-existent ID")]
        [TestCase("invalid-id", Description = "Text non-existent ID")]
        [TestCase("00000000-0000-0000-0000-000000000000", Description = "Zero GUID")]
        [Category("Delete")]
        [Category("Error")]
        public async Task GivenNonExistentId_WhenDeleteObject_ThenThrowsHttpRequestException(string nonExistentId)
        {
            // Act & Assert
            var act = () => ObjectsService.DeleteObjectAsync(Client, nonExistentId);
            await act.Should()
                .ThrowAsync<HttpRequestException>()
                .WithMessage("*404*")
                .ConfigureAwait(false);
        }

        [TestCase("", Description = "Empty string")]
        //[TestCase(" ", Description = "Space")]
        [Category("Delete")]
        [Category("Validation")]
        public async Task GivenInvalidId_WhenDeleteObject_ThenThrowsArgumentException(string invalidId)
        {
            // Act & Assert
            var act = () => ObjectsService.DeleteObjectAsync(Client, invalidId);
            await act.Should()
                .ThrowAsync<JsonException>()
                .WithMessage("*Object with id =   doesn't exist.*")
                .ConfigureAwait(false);
        }

        [Test]
        public async Task GivenWhiteSpaceID_WhenDeleteObject_ThenThrowsArgumentException()
        {
            // Arrange
            const string whitespaceId = " ";

            // Act & Assert
            var act = () => ObjectsService.DeleteObjectAsync(Client, whitespaceId);

            await act.Should().ThrowAsync<JsonException>()
                .WithMessage("*Object with id =   doesn't exist.*")
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
