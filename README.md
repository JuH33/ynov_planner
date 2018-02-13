# Planner API

This project has been initialized by the wild group Ynov. Its objective is to provide a Multiplateforme Api for Event Management.
The stack: 
 - ASP.NET Core
 - Entity Framwork
 - Xunit
 - JWT (HmacSha256)
 
# Run Application

It'll be necessary to have dotnet 2.0 and mysql installed on your server or local device. 

## Command tools

 1. dotnet restore
 2. dotnet ef database update
 3. dotnet run

If you encounter problem, ensure all tools and package are properly installed.

# Run Unit Tests

Units tests are localized in the PlannerApi.Tests folder from the base solution. It merge a complete solution of Unit, Integration, Functional tests.

### Command tools

 1. cd .../PlannerApi.Tests
 2. dotnet xunit

Running on mac or windows with visual studio, you can run the tests directly from the UI of your IDE.
