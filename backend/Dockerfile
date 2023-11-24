# Use the .NET SDK image as a base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy only necessary files for building
COPY backend/backend.csproj ./backend/
COPY backend.Tests/backend.Tests.csproj ./backend.Tests/

# Restore dependencies for backend
RUN dotnet restore ./backend/backend.csproj

# Restore dependencies for backend.Tests
RUN dotnet restore ./backend.Tests/backend.Tests.csproj

# Copy the entire solution
COPY . .

# Build the application
WORKDIR /app/backend
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/backend/out ./

# Install SQLite
RUN apt-get update && apt-get install -y sqlite3

# Expose the port your application will run on
EXPOSE 8080

# Add a custom entrypoint script
COPY backend/entrypoint.sh .
RUN chmod +x entrypoint.sh

# Copy the SQL file
COPY backend/init.sql /app/

# Set the entry point for the container
ENTRYPOINT ["./entrypoint.sh"]
