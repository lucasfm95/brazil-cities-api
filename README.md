# Brazil Cities API

This is a .NET 8 project that provides a RESTful API for managing Brazil cities. The project is written in C# and uses several technologies and frameworks.

[![Build](https://github.com/lucasfm95/brazil-cities-api/actions/workflows/build.yml/badge.svg)](https://github.com/lucasfm95/brazil-cities-api/actions/workflows/build.yml)

## Try it out

The API is hosted on Render. You can access it at [https://brazil-cities-api.onrender.com](https://brazil-cities-api.onrender.com).

Swagger documentation is available at [https://brazil-cities-api.onrender.com/swagger](https://brazil-cities-api.onrender.com/swagger/index.html).

## Technologies Used

- **.NET 8**: The latest version of the .NET framework, used for building high-performance, cross-platform applications.
- **C#**: The primary programming language used in this project.
- **ASP.NET Core**: A framework for building web applications.
- **Entity Framework Core**: An object-relational mapper (ORM) that simplifies data access by letting you work with relational data using domain-specific objects.
- **PostgresSQL**: The database used for persisting data.

## Getting Started With Docker Compose

1. Clone the repository.
2. Navigate to the root directory.
3. Ensure Docker is installed on your machine.
4. Create a .env file in the root directory like .env.example file.
    1. Add the environment variables.
5. Run command `docker-compose up -d` in the terminal
6. The API be available at `http://localhost:8080`
7. Execute InitializeDataDb app to initialize database data.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.