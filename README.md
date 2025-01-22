# Task Management System

A Domain-Driven Design (DDD) implementation of a Task Management System using .NET 9. This project demonstrates clean architecture and DDD principles.

## Project Structure

The solution follows a clean architecture with DDD tactical patterns:

- **DDD_Project.Domain**: Core domain logic
  - Aggregates (Task, Comments, Labels)
  - Value Objects
  - Domain Events
  - Repository Interfaces
  
- **DDD_Project.Application**: Application services and use cases
  - Commands and Queries (CQRS)
  - DTOs
  - Interfaces for Infrastructure services
  
- **DDD_Project.Infrastructure**: Implementation details
  - Database Context and Configurations
  - Repository Implementations
  - External Service Integrations
  
- **DDD_Project.API**: Web API
  - Controllers
  - API Models
  - Middleware

## Domain Model

### Task Aggregate
- Task (Aggregate Root)
  - Title
  - Description
  - Priority (Low, Medium, High, Critical)
  - Status (ToDo, InProgress, UnderReview, Done, Archived)
  - Due Date
  - Assigned User
  - Comments
  - Labels

### Value Objects
- TaskComment
  - Content
  - Author
  - Creation Date
  
- TaskLabel
  - Name
  - Color

## Getting Started

### Prerequisites
- .NET 9 SDK
- Your favorite IDE (Visual Studio, VS Code, Rider)

### Setup
1. Clone the repository
```bash
git clone https://github.com/yourusername/DDD_Project.git
```

2. Navigate to the project directory
```bash
cd DDD_Project
```

3. Build the solution
```bash
dotnet build
```

4. Run the API
```bash
cd DDD_Project.API
dotnet run
```

## Development Patterns and Practices

- **Domain-Driven Design**: Focus on the core domain and domain logic
- **Clean Architecture**: Separation of concerns with layers
- **CQRS**: Separation of read and write operations
- **Repository Pattern**: Abstraction of data persistence
- **Rich Domain Model**: Business logic encapsulated in the domain
- **Domain Events**: For handling cross-cutting concerns