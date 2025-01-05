#Build
FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build
WORKDIR /app
COPY . .
RUN dotnet publish RedditClone/Server -c Release -o out

#Serve
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim
WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 2345

ENTRYPOINT ["dotnet", "RedditClone.Server.dll", "--urls", "http://0.0.0.0:2345"]