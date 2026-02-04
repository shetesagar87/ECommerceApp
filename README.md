# ECommerceApp (ASP.NET Core MVC)

This is a minimal, production-oriented starter for an e-commerce app using ASP.NET Core 7, EF Core, SQL Server, Razor Views, and Bootstrap.

Quick start:

1. Ensure .NET 7 SDK is installed.
2. Restore packages and build:

```powershell
dotnet restore
dotnet build
















- Program.cs — app startup and DI registration
- appsettings.json — connection string
- DAL/ApplicationDbContext.cs — EF Core DbContext
- Models/Product.cs — product entity
- Repositories/* — repository layer
- Services/* — service layer
- Controllers/* and Views/* — MVC UIFiles of interest:dotnet run
``````powershell4. Run:dotnet ef database update
```dotnet ef migrations add InitialCreatedotnet tool install --global dotnet-ef```powershell3. To create the database and apply EF migrations (install dotnet-ef if needed):```