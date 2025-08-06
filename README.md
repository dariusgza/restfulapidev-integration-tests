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

For comprehensive architecture details, design patterns, and system diagrams, see:

📖 **[Architecture Design Document](docs/ArchitectureDesign.md)**

### 🔧 Key Components

- **Layered Architecture**: Service layer, HTTP client layer, and data models
- **Design Patterns**: Builder, Service Layer, and Template Method patterns
- **Test Infrastructure**: Automated setup, cleanup, and resource management

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

For detailed test scenarios and comprehensive coverage analysis, see:

📖 **[Test Scenarios Document](docs/TestScenarios.md)**

### 🏷️ Quick Reference

- `Integration` - All integration tests | `Read` - GET operations  
- `Create` - POST operations | `Update` - PUT/PATCH operations
- `Delete` - DELETE operations | `Validation` - Input validation | `Error` - Error handling

---

## 🚀 CI/CD Pipeline

For detailed testing strategy, execution phases, and quality gates, see:

📖 **[Test Plan Document](docs/TestPlan.md)**
📖 **[Architecture Design Document - CI/CD Section](docs/ArchitectureDesign.md#-deployment-architecture)**

### ⚡ Pipeline Overview

- **Multi-Stage Testing**: .NET tests, Postman collection, security scanning
- **Parallel Execution**: Optimized for speed with concurrent job execution  
- **Comprehensive Reporting**: Automated summaries and artifact collection
- **Quality Gates**: 95%+ pass rate requirement with detailed failure analysis

---

## 🛠️ Development Workflow

For detailed development guidelines, code patterns, and maintenance procedures, see:

📖 **[Test Plan Document - Development Workflow](docs/TestPlan.md#-maintenance--evolution)**
📖 **[Architecture Design Document - Design Patterns](docs/ArchitectureDesign.md#-design-patterns--principles)**

### 🧪 Quick Start

1. **Extend TestBase** for automatic setup/cleanup
2. **Use Test Builders** for flexible test data creation
3. **Follow Given-When-Then** naming conventions for test methods
4. **Leverage FluentAssertions** for expressive test assertions

---

## 📊 Test Results & Reporting

For comprehensive reporting strategy, metrics, and KPIs, see:

📖 **[Test Plan Document - Reporting & Metrics](docs/TestPlan.md#-reporting--metrics)**

### 📋 Quick Overview

- **Local**: TRX format results in `IntegrationTests/TestResults/`
- **CI/CD**: GitHub Actions summaries, downloadable artifacts, and PR integration
- **Security**: OWASP ZAP reports in multiple formats

---

## ⚠️ Known Limitations

For comprehensive risk management and mitigation strategies, see:

📖 **[Test Plan Document - Risk Management](docs/TestPlan.md#-risk-management)**

### 🚦 Key Limitations

- **API Rate Limiting**: 100 requests/day limit with automatic detection and graceful handling
- **Network Dependencies**: External API dependency with retry mechanisms in place

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

## 📚 Documentation & Resources

### 📖 Project Documentation

- **[Architecture Design Document](docs/ArchitectureDesign.md)** - System architecture, design patterns, and component interactions
- **[Test Plan Document](docs/TestPlan.md)** - Comprehensive testing strategy, execution phases, and quality gates
- **[Test Scenarios Document](docs/TestScenarios.md)** - Detailed test cases and coverage analysis

### 🔗 External Resources

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