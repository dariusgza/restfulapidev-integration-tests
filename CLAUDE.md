# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a .NET 9 C# integration test suite for the RESTFUL-API.dev service. It uses NUnit as the testing framework with FluentAssertions for readable test assertions. The project includes both automated .NET tests and Postman collection tests executed via Newman.

## Common Commands

### Build and Test
```bash
# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run all integration tests
dotnet test

# Run tests with specific output format
dotnet test --logger "trx;LogFileName=integration-test-results.trx"

# Build in Release configuration
dotnet build --configuration Release

# Run tests without rebuilding
dotnet test --no-build --configuration Release
```

### Manual Testing
```bash
# Run Postman collection with Newman
newman run ./src/ManualTests/PostmanCollection/api.restfull.dev.demo.postman_collection.json --reporters cli,junit --reporter-junit-export=postman-results.xml
```

## Architecture

### Core Framework Components

- **HttpClientFactory** (`IntegrationTests/Framework/HttpClientFactory.cs`): Creates configured HttpClient instances with base URL set to `https://api.restful-api.dev/`
- **ObjectsClient** (`IntegrationTests/Framework/ObjectsClient.cs`): Generic HTTP client wrapper that handles JSON serialization/deserialization and provides methods for GET, POST, PUT, PATCH, DELETE operations
- **ObjectsService** (`IntegrationTests/Framework/Services/ObjectsService.cs`): Service layer that wraps ObjectsClient calls and provides business logic validation

### Test Infrastructure

- **TestBase** (`IntegrationTests/Tests/TestBase.cs`): Base class for all tests providing:
  - Setup/teardown with automatic cleanup of created test objects
  - Object tracking to prevent test pollution
  - Common assertion helpers for response validation
- **Test Builders** (`IntegrationTests/TestBuilders/`): Builder pattern implementations for creating test data objects

### Data Models

All models are in `IntegrationTests/Framework/Models/`:
- **Objects**: Main API entity with Id, Name, and Data properties
- **ObjectsRequest/ObjectsResponse**: Request/response wrappers
- **ObjectsAttributes**: Flexible attribute container using custom JSON converter
- **DeleteResponse**: Response model for delete operations

### Test Organization

Tests are categorized using NUnit categories:
- `Integration`: All integration tests
- `Read`: GET operations
- `Create`: POST operations  
- `Update`: PUT/PATCH operations
- `Delete`: DELETE operations
- `Validation`: Input validation tests
- `Error`: Error handling tests

## Development Workflow

### Running Single Tests
```bash
# Run tests by category
dotnet test --filter Category=Create

# Run specific test class
dotnet test --filter ClassName~ObjectsCrudTests

# Run single test method
dotnet test --filter Name~GivenValidRequest_WhenPostObject_ThenReturnsCreatedObject
```

### CI/CD Pipeline

The GitHub Actions workflow (`.github/workflows/run-tests.yml`) runs:
1. .NET integration tests with test result publishing
2. Postman collection tests via Newman
3. ZAP security scanning against the API
4. SonarCloud analysis (currently disabled)

### Key Design Patterns

- **Service Layer Pattern**: ObjectsService provides abstraction over HTTP operations
- **Builder Pattern**: Test builders for creating complex test data
- **Template Method Pattern**: TestBase provides common setup/teardown behavior
- **Repository Pattern**: ObjectsClient acts as repository for API operations

### API Target

Tests run against `https://api.restful-api.dev/` endpoints:
- Base endpoint: `/objects`
- Operations: GET, POST, PUT, PATCH, DELETE on objects resource

### Error Handling

The framework expects and tests for:
- HTTP 404 responses for non-existent resources
- Argument validation exceptions for invalid inputs
- JSON deserialization errors for malformed responses
- Automatic cleanup of test artifacts even when tests fail