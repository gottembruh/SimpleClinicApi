<p align="center">
  <img src="./Logo.png" alt="SimpleClinicApi Logo" width="200" />
</p>

# 🏥 SimpleClinicApi

Welcome to **SimpleClinicApi** — a sandbox project for practicing API development with ASP.NET Core 9 using modern
architectural patterns such as MediatR, CQRS, Repository pattern, and layered architecture.

---

## 🚀 Project Overview

The solution is organized into multiple projects representing different architectural layers:

| Project                                                                                                                      | Description                                                                              |
|------------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------|
| **[SimpleClinicApi](https://github.com/gottembruh/SimpleClinicApi/tree/main/SimpleClinicApi)**                               | ASP.NET Core 9 Web API project — Presentation layer with controllers and API entrypoints |
| **[SimpleClinicApi.Domain](https://github.com/gottembruh/SimpleClinicApi/tree/main/SimpleClinicApi.Domain)**                 | Domain layer with entities, repository interfaces, and core business logic               |
| **[SimpleClinicApi.DataAccess](https://github.com/gottembruh/SimpleClinicApi/tree/main/SimpleClinicApi.DataAccess)**         | Data access layer, including Entity Framework Core DbContext and migrations              |
| **[SimpleClinicApi.Infrastructure](https://github.com/gottembruh/SimpleClinicApi/tree/main/SimpleClinicApi.Infrastructure)** | Infrastructure services, external integrations, and configuration                        |
| **[SimpleClinicApi.Tests](https://github.com/gottembruh/SimpleClinicApi/tree/main/SimpleClinicApi.Tests)**                   | Unit and integration tests                                                               |

This layered approach promotes clear separation of concerns, improves maintainability, and facilitates testing.

---

## 🛠️ Technologies & Patterns

- ASP.NET Core 9
- MediatR (CQRS implementation)
- FluentValidation for model validation
- Entity Framework Core 9 with SQLite database provider
- Swashbuckle for Swagger/OpenAPI documentation
- Repository pattern for data abstraction

---

## 🎯 Project Goals

- Learn and apply CQRS with MediatR for separation between commands and queries.
- Build scalable and maintainable APIs with clean layered architecture.
- Practice implementing repository pattern for data access abstraction.
- Implement robust validation and comprehensive error handling.

---

## 📦 Building and Running

**1. Clone this repository:**

  `git clone https://github.com/gottembruh/SimpleClinicApi.git`

**2. Run the API in a Docker container with the provided Dockerfile and docker-compose:**

`docker-compose up --build`

**3. Open http://localhost:8080/swagger/index.html in the browser**



<h3>**Or** Build the solution locally: </h2>

**1. **Select _Build_ project from Run Configuration****

**2. Run!**


---

## 🤝 Contributing

This is primarily a personal sandbox project, but contributions, suggestions, and pull requests are warmly welcomed.

---

## 📜 License

This project is released under the MIT License. See the LICENSE file for details.

---

Thank you for checking out SimpleClinicApi! Happy coding and learning! 🎉

---

