# HelperPlan Backend
Project Frontend Repository Link (https://github.com/PoulaMaher/HelperPlan-Angular-.git)
This backend service for the HelperPlan project is developed using .NET Web API Core. It provides various APIs for managing helpers, drivers, employers, jobs, and more.

## Getting Started

### Prerequisites

- .NET SDK 6.0 or later
- SQL Server or any other database you plan to use
- Visual Studio or Visual Studio Code

### Installation

1. **Clone the repository:**

   ```bash
   git clone <your-backend-repo-url>
   cd HelperPlan-Backend
Install dependencies:

If your project has any dependencies that need to be restored, run:

bash

dotnet restore

Update the database connection string:

Update the appsettings.json file with your database connection string:

json

    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=<server>;Database=<database>;User Id=<user>;Password=<password>;"
      }
    }

###Running the Application

    Build the project:

    bash

dotnet build

Apply migrations and update the database:

If you are using Entity Framework Core for database migrations, run:

bash

dotnet ef database update

Run the application:

bash

    dotnet run

    The application will start on http://localhost:5000 by default.

###API Documentation

The API documentation is available through Swagger. Once the application is running, navigate to http://localhost:5000/swagger to view and interact with the API endpoints.
Project Structure

Here is an overview of the project structure:

    Controllers: Contains the API controllers.
    Models: Contains the data models.
    Data: Contains the DbContext and data migrations.
    Services: Contains business logic and services.

Building and Testing
Build

To build the project, run:

bash

dotnet build

Running Unit Tests

To run unit tests, use the following command:

bash

dotnet test

###Running Integration Tests

If you have integration tests, run them using:

bash

dotnet test --filter Integration

###Deployment

To deploy the application, you can use various methods like Docker, Azure, AWS, etc. Here is a basic Dockerfile for containerizing your application:

Dockerfile

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HelperPlan.csproj", "./"]
RUN dotnet restore "./HelperPlan.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "HelperPlan.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HelperPlan.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HelperPlan.dll"]

Build and run the Docker image:

bash

docker build -t helperplan-backend .
docker run -d -p 80:80 helperplan-backend

Contributing

Contributions are welcome! Please open an issue or submit a pull request with your changes.
License

This project is licensed under the MIT License.
