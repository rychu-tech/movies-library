# Use the .NET SDK image as a base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the .csproj file and restore dependencies
COPY *.csproj .
RUN dotnet restore

# Copy the rest of the application code
COPY . .

# Build the application
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Install SQLite
RUN apt-get update && apt-get install -y sqlite3

# Expose the port your application will run on
EXPOSE 8080

# Add a custom entrypoint script
COPY entrypoint.sh .
RUN chmod +x entrypoint.sh

# Copy the SQL file
COPY init.sql /app/

# Set the entry point for the container
ENTRYPOINT ["./entrypoint.sh"]
