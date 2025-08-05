# 🧪 RESTFUL-API.dev Integration Tests

A comprehensive .NET 9 integration test suite for the [RESTFUL-API.dev](https://restful-api.dev/) service, demonstrating modern testing patterns, CI/CD practices, and automated quality assurance.

## 🚀 Build Status

| Service        | Status                                                                                                                                                                                                                                                 |
| -------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| **GitHub Actions** | [![Run .NET API Integration Tests](https://github.com/dariusgza/restfulapidev-integration-tests/actions/workflows/run-tests.yml/badge.svg)](https://github.com/dariusgza/restfulapidev-integration-tests/actions/workflows/run-tests.yml) |
| **SonarCloud** | [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=dariusgza_restfulapidev-integration-tests&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=dariusgza_restfulapidev-integration-tests) |

---

## 📖 Project Overview

This project provides a complete integration testing solution for REST APIs, featuring:

- **🏗️ Modern Architecture**: Clean separation of concerns with service layers, clients, and test builders
- **🧪 Comprehensive Testing**: Full CRUD operations testing with validation and error handling
- **📊 Multiple Test Approaches**: Both .NET NUnit tests and Postman/Newman collection tests
- **🔒 Security Testing**: Automated OWASP ZAP security scanning
- **🚀 CI/CD Pipeline**: Optimized GitHub Actions workflow with parallel execution
- **📈 Quality Assurance**: FluentAssertions for readable assertions and comprehensive reporting

### 🎯 Key Features

- **HTTP Client Framework**: Reusable, configurable HTTP client with JSON serialization
- **Service Layer Pattern**: Business logic abstraction over HTTP operations  
- **Builder Pattern**: Flexible test data construction
- **Automated Cleanup**: Prevents test pollution with automatic resource management
- **Parallel Execution**: Optimized CI/CD pipeline for faster feedback
- **Comprehensive Reporting**: Test results, security scans, and quality metrics

---

## 🏛️ Architecture

### 📁 Project Structure

```
IntegrationTests/
├── Framework/
│   ├── HttpClientFactory.cs      # HTTP client configuration
│   ├── ObjectsClient.cs           # Generic HTTP client wrapper
│   ├── Models/                    # Data transfer objects
│   └── Services/
│       └── ObjectsService.cs      # Business logic layer
├── TestBuilders/                  # Builder pattern implementations
│   ├── ObjectsRequestBuilder.cs
│   └── ObjectsAttributesBuilder.cs
└── Tests/
    ├── TestBase.cs               # Base class with setup/cleanup
    └── ObjectsCrudTests.cs       # Integration test suite
```

### 🔧 Core Components

- **HttpClientFactory**: Creates configured HttpClient instances with base URL
- **ObjectsClient**: Generic HTTP client with JSON serialization/deserialization
- **ObjectsService**: Service layer providing business logic validation
- **TestBase**: Common test infrastructure with automatic cleanup
- **Test Builders**: Fluent APIs for creating test data

---

## 🚀 Getting Started

### 📋 Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) or later
- [Node.js 20+](https://nodejs.org/) (for Newman/Postman tests)
- Git for cloning the repository

### ⚙️ Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/dariusgza/restfulapidev-integration-tests.git
   cd restfulapidev-integration-tests
   ```

2. **Restore dependencies and build:**
   ```bash
   dotnet restore IntegrationTests/restfulapidev-integration-tests.csproj
   dotnet build IntegrationTests/restfulapidev-integration-tests.csproj --configuration Release
   ```

---

## 🧪 Running Tests

### .NET Integration Tests

Run all NUnit tests:
```bash
# Run all tests
dotnet test IntegrationTests/restfulapidev-integration-tests.csproj

# Run with detailed output
dotnet test IntegrationTests/restfulapidev-integration-tests.csproj --logger "console;verbosity=normal"

# Run specific test categories
dotnet test --filter Category=Create
dotnet test --filter Category=Read
dotnet test --filter Category=Update
dotnet test --filter Category=Delete
```

### Postman/Newman Tests

Run the Postman collection:
```bash
# Install Newman globally
npm install -g newman

# Run Postman collection
newman run ./src/ManualTests/PostmanCollection/api.restfull.dev.demo.postman_collection.json --reporters cli,junit
```

---

## 🔍 Test Categories

Tests are organized using NUnit categories:

| Category | Description | Tests |
|----------|-------------|--------|
| `Integration` | All integration tests | All |
| `Read` | GET operations | Object retrieval, validation |
| `Create` | POST operations | Object creation, validation |
| `Update` | PUT/PATCH operations | Object modification |
| `Delete` | DELETE operations | Object removal |
| `Validation` | Input validation | Parameter validation tests |
| `Error` | Error handling | HTTP error responses |

---

## 🚀 CI/CD Pipeline

The GitHub Actions workflow provides comprehensive automated testing:

### 🏗️ Pipeline Jobs

1. **📊 .NET Integration Tests**
   - Dependency caching for faster builds
   - Comprehensive test execution
   - Test result publishing and artifacts

2. **🌐 Postman API Tests**
   - Newman collection execution
   - Rate limiting protection
   - Detailed reporting

3. **🔒 Security Scanning**
   - OWASP ZAP baseline security scan
   - Vulnerability reporting
   - Security artifact generation

4. **📈 Test Summary**
   - Aggregated results from all jobs
   - Clear pass/fail indicators
   - Comprehensive workflow reporting

### 🎯 Pipeline Features

- **⚡ Parallel Execution**: Jobs run concurrently where possible
- **📦 Intelligent Caching**: NuGet packages cached for faster builds
- **🛡️ Error Handling**: Graceful handling of API rate limits
- **📊 Rich Reporting**: Detailed summaries and artifacts
- **🔄 Flexible Triggers**: Push, PR, manual, and scheduled execution

---

## 🛠️ Development Workflow

### 🧪 Writing Tests

1. **Extend TestBase** for automatic setup/cleanup:
   ```csharp
   [TestFixture]
   [Category("Integration")]
   public class MyApiTests : TestBase
   {
       // Tests automatically inherit Client and cleanup
   }
   ```

2. **Use Test Builders** for data creation:
   ```csharp
   var request = ObjectsRequestBuilder.Create()
       .WithName("test-object")
       .WithData(builder => builder.WithDescription("Test description"))
       .Build();
   ```

3. **Follow Naming Conventions**:
   ```csharp
   [Test]
   public async Task GivenValidRequest_WhenCallingApi_ThenReturnsExpectedResult()
   ```

### 📋 Code Quality

- **FluentAssertions**: Use expressive assertions for better readability
- **Async/Await**: All HTTP operations are properly async
- **ConfigureAwait(false)**: Used consistently for library code
- **Resource Cleanup**: Automatic cleanup prevents test pollution

---

## 📊 Test Results & Reporting

### 🎯 Local Testing

Test results are generated in TRX format:
- Location: `IntegrationTests/TestResults/`
- Format: Visual Studio Test Results (`.trx`)
- Viewable in Visual Studio or with TRX viewers

### 🌐 CI/CD Reporting

- **GitHub Actions Summary**: Detailed results in workflow summaries
- **Test Artifacts**: Downloadable test result files
- **Security Reports**: ZAP scan results in multiple formats
- **PR Comments**: Automated test result comments on pull requests

---

## ⚠️ Known Limitations

### 🚦 API Rate Limiting

The RESTFUL-API.dev service has limitations:
- **100 requests per day** per IP
- Tests may fail when rate limit is exceeded
- Rate limit resets daily
- Use `--delay-request` with Newman to reduce rate limiting

### 🛠️ Workarounds

- Tests include rate limiting detection
- CI/CD pipeline includes delays between requests
- Graceful error handling for rate limit scenarios

---

## 🤝 Contributing

### 📝 Guidelines

1. **Follow Patterns**: Use existing architectural patterns
2. **Add Tests**: New features require corresponding tests
3. **Update Documentation**: Keep README and CLAUDE.md updated
4. **Use Categories**: Tag tests appropriately
5. **Handle Cleanup**: Ensure proper resource cleanup

### 🔧 Development Commands

```bash
# Run specific test class
dotnet test --filter ClassName~ObjectsCrudTests

# Run with coverage (if configured)
dotnet test --collect:"XPlat Code Coverage"

# Build in Release mode
dotnet build --configuration Release
```

---

## 📚 Additional Resources

- **[RESTFUL-API.dev Documentation](https://restful-api.dev/)**
- **[NUnit Documentation](https://docs.nunit.org/)**
- **[FluentAssertions Documentation](https://fluentassertions.com/)**
- **[GitHub Actions Documentation](https://docs.github.com/en/actions)**

---

## 📜 License

This project is open source and available under the [MIT License](LICENSE).

---

## 🆘 Support

For questions, issues, or contributions:
- **Issues**: [GitHub Issues](https://github.com/dariusgza/restfulapidev-integration-tests/issues)
- **Discussions**: [GitHub Discussions](https://github.com/dariusgza/restfulapidev-integration-tests/discussions)
- **CI/CD Help**: Check [CLAUDE.md](CLAUDE.md) for detailed development guidance