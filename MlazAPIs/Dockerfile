# Stage 1: Build the project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy everything
COPY . .

# Restore dependencies
RUN dotnet restore

# Publish project
RUN dotnet publish -c Release -o /app

# Stage 2: Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copy published files
COPY --from=build /app .

# Expose port 8080 for Render
EXPOSE 8080

# Tell ASP.NET Core to listen on 0.0.0.0:8080
ENV ASPNETCORE_URLS=http://0.0.0.0:8080

# Run the app
ENTRYPOINT ["dotnet", "MlazAPIs.dll"]
