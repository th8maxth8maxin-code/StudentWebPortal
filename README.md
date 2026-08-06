# StudentWebPortal API

A robust RESTful Web API built with **ASP.NET Core** and **Entity Framework Core**, providing complete CRUD operations for managing student data, database migrations, and interactive API documentation through **Swagger / OpenAPI**.

---

## 🚀 Features

- **RESTful Endpoints** – Full CRUD operations for managing student records.
- **GUID-Based Routing** – Strongly typed unique identifiers (`/api/students/{id:guid}`).
- **Entity Framework Core** – Code-First development with migrations and database version tracking.
- **Interactive API Documentation** – Built-in Swagger UI for testing endpoints directly from the browser.
- **Environment-Based Configuration** – Database connection strings managed using `appsettings.json` and `appsettings.Development.json`.

---

## 🛠 Tech Stack

| Technology | Description |
|------------|-------------|
| **Framework** | ASP.NET Core Web API (.NET 10) |
| **ORM** | Entity Framework Core |
| **Database** | PostgreSQL |
| **API Documentation** | Swagger / OpenAPI |
| **Tools** | .NET CLI, `dotnet-ef` |

---

# Project Setup & Local Development

## 1. Prerequisites

Ensure the following are installed on your system:

- .NET 10 SDK
- PostgreSQL
- Git

Install or update the Entity Framework Core CLI tools:

```bash
dotnet tool update --global dotnet-ef
```

---

## 2. Getting Started

### Clone the Repository

```bash
git clone https://github.com/YOUR_USERNAME/StudentWebPortal.git
cd StudentWebPortal
```

---

### Configure the Database Connection

Update the connection string in **appsettings.Development.json**.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=StudentDb;Username=postgres;Password=YourPassword;"
  }
}
```

Replace the following values with your own:

- `localhost` → PostgreSQL server host
- `5432` → PostgreSQL port (default)
- `StudentDb` → Database name
- `postgres` → Database username
- `YourPassword` → Database password

---

### Apply Database Migrations

Create and update the database schema.

```bash
dotnet ef database update
```

---

### Run the Application

```bash
dotnet run
```

---

# 📖 API Documentation

After the application starts, open your browser and navigate to:

```
http://localhost:5000/swagger
```

or the HTTPS URL displayed in your terminal, for example:

```
https://localhost:7000/swagger
```

Swagger provides interactive documentation for testing all available API endpoints.

---

# 📌 API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/students` | Retrieve all students |
| GET | `/api/students/{id}` | Retrieve a student by GUID |
| POST | `/api/students` | Create a new student |
| PUT | `/api/students/{id}` | Update an existing student |
| DELETE | `/api/students/{id}` | Delete a student |

---

## Example Request

### Get Student by ID

```http
GET /api/students/3fa85f64-5717-4562-b3fc-2c963f66afa6
```

---

# 📂 Project Structure

```text
StudentWebPortal/
├── Controllers/
├── Data/
├── DTOs/
├── Migrations/
├── Models/
├── Services/
├── appsettings.json
├── appsettings.Development.json
├── Program.cs
└── StudentWebPortal.csproj
```

---

# 📄 License

This project is licensed under the **MIT License**.

See the [LICENSE](LICENSE) file for more details.