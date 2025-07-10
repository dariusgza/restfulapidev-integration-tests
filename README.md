
**WIP**
# restfulapidev-integration-tests

## Build Status

| Service        | Status                                                                                                                                                                                                                                                 |
| -------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| **GitHub**     | [![Run .NET API Integration Tests](https://github.com/dariusgza/restfulapidev-integration-tests/actions/workflows/run-tests.yml/badge.svg)](https://github.com/dariusgza/restfulapidev-integration-tests/actions/workflows/run-tests.yml)
| **SonarCloud** | [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=dariusgza_restfulapidev-integration-tests&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=dariusgza_restfulapidev-integration-tests)  

---

## Project Overview

This project contains C# integration tests for the [RESTFUL-API](https://restful-api.dev/) built using NUnit. The tests demonstrate:

- A reusable HTTP client framework
- FluentAssertions for expressive and maintainable assertions
- Continuous Integration via Github Actions and static code analysis with SonarCloud

---

## Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) or later
- Postman to execute the collection (WIP)

### Setup

1. Clone the repository:
    ```bash
    git clone https://github.com/dariusgza/restfulapidev-integration-tests.git
    cd restfulapidev-integration-tests
    ```
2. Restore dependencies and build:
   ```bash
   dotnet restore
   dotnet build
   ```

---

## 🚀 Running Tests

Run all tests using the .NET CLI:

```bash
dotnet test
```

This will execute the NUnit tests

---