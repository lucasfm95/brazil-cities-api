# Brazil Cities API

A RESTful API for managing Brazilian states and cities, built with .NET 10 and C#.

[![CI/CD](https://github.com/lucasfm95/brazil-cities-api/actions/workflows/ci-cd.yml/badge.svg)](https://github.com/lucasfm95/brazil-cities-api/actions/workflows/ci-cd.yml)

## Try it out

The API is hosted on Render. You can access it at [https://brazil-cities-api.onrender.com](https://brazil-cities-api.onrender.com).

Documentation is available at [Swagger UI](https://brazil-cities-api.onrender.com/swagger/index.html) or [Scalar](https://brazil-cities-api.onrender.com/scalar/v1).

## Technologies Used

- **.NET 10** / **C#**: The runtime and language used in all projects of the solution.
- **ASP.NET Core**: Web framework, with API versioning (`Asp.Versioning`), OpenAPI, Swagger UI and Scalar.
- **Entity Framework Core 10**: ORM and database versioning through migrations (Npgsql provider).
- **PostgreSQL**: The database used for persisting data.
- **Redis**: Cache for lookups by id.
- **Docker / Docker Compose**: Containerized setup for the API, PostgreSQL and Redis.
- **GitHub Actions**: CI/CD pipeline that builds, tests and publishes the image to GHCR.

## Project Structure

| Project | Description |
|---|---|
| `src/BrazilCities.Api` | Controllers, startup, health check and rate limiting |
| `src/BrazilCities.Application` | Services and repository/cache abstractions |
| `src/BrazilCities.Domain` | Entities, requests and responses |
| `src/BrazilCities.Persistence` | `AppDbContext`, EF Core configurations, migrations, repositories and Redis cache |
| `InitializeDataDb` | Console app that loads all Brazilian states and cities through the API |
| `k6` | Load test script |

## Endpoints

All endpoints are versioned in the URL (`/api/v1/...`) and limited to 10 requests per minute per IP.

| Resource | Endpoints |
|---|---|
| States | `GET /api/v1/states`, `GET /api/v1/states/{id}`, `GET /api/v1/states/acronym/{acronym}`, `POST`, `PUT /{id}`, `DELETE /{id}` |
| Cities | `GET /api/v1/cities`, `GET /api/v1/cities/{id}`, `POST`, `PUT /{id}`, `DELETE /{id}` |

The health check (API and PostgreSQL status) is available at `/HealthCheck`.

## Configuration

The API is configured through environment variables (see `.env.example`):

| Variable | Description |
|---|---|
| `CONNECTION_STRING_DB_POSTGRES` | PostgreSQL connection string used by the API |
| `CONNECTION_STRING_REDIS` | Redis connection string used by the API |
| `POSTGRES_USER`, `POSTGRES_PASSWORD`, `POSTGRES_DB` | Credentials and database name used by the PostgreSQL container (Docker Compose only) |

## Getting Started With Docker Compose

1. Clone the repository.
2. Navigate to the root directory.
3. Ensure Docker is installed on your machine.
4. Create a `.env` file in the root directory like the `.env.example` file.
    1. Add the environment variables.
5. Run command `docker compose up -d` in the terminal.
6. The API will be available at `http://localhost:8080`.
7. Execute the `InitializeDataDb` app to initialize database data (`dotnet run --project InitializeDataDb`).

## Running Locally

Requirements: [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0), a PostgreSQL database and a Redis instance.

1. Set `CONNECTION_STRING_DB_POSTGRES` and `CONNECTION_STRING_REDIS` (or adjust `src/BrazilCities.Api/Properties/launchSettings.json`, which overrides them when running with the launch profile).
2. Run `dotnet run --project src/BrazilCities.Api`.

## Database Migrations

The database schema is managed with EF Core migrations, located in `src/BrazilCities.Persistence/Migrations`.

**Migrations are applied automatically when the API starts.** The API checks for pending migrations and applies them, so a brand new database that only exists (with no tables) is set up on first run. If the database is not reachable yet, the API retries a few times before failing.

To create a new migration after changing the entities (requires the `dotnet-ef` tool):

```bash
export CONNECTION_STRING_DB_POSTGRES="Host=localhost;Database=cities;Username=...;Password=..."
dotnet ef migrations add <MigrationName> --project src/BrazilCities.Persistence
```

## Docker Image

The CI/CD pipeline publishes the image (`linux/amd64` and `linux/arm64`) to GitHub Container Registry on every push to `main`:

```bash
docker pull ghcr.io/lucasfm95/brazil-cities-api:latest
```

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
