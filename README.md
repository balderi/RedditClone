# ASP.NET Core 6 WebAPI with authentication
A barebones ASP.NET hosted Blazor WebAssembly app, using JWT bearer tokens for authenticating users.
Built for Microsoft SQL Server and Mircrosoft SQL Server Express databases, but can easily be modified for MySQL/MariaDB use.

## How do?
After cloning the repository, add your SQL Server connection string and secret key (can be literally anything, as long as it is at least 16 characters long) in the `appsettings.json` file in the `WebApiWithAuth.Server` project. Then run `dotnet ef database update` from the `WebApiWithAuth.Server` project directory, with VisualStudio's Package Manager Console, PowerShell, or similar.
