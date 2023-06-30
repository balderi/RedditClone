# Reddit-like message board
Just a fun hobby project.

## How do?
After cloning the repository, add your SQL Server connection string and secret key (can be literally anything, as long as it is at least 16 characters long) in the `appsettings.json` file in the `RedditClone.Server` project. Then run `dotnet ef database update` from the `RedditClone.Server` project directory, with VisualStudio's Package Manager Console, PowerShell, or similar.
