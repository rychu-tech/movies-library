FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY backend/backend.csproj ./backend/
COPY backend.Tests/backend.Tests.csproj ./backend.Tests/

RUN dotnet restore ./backend/backend.csproj

RUN dotnet restore ./backend.Tests/backend.Tests.csproj

COPY . .

WORKDIR /app/backend
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/backend/out ./

RUN apt-get update && apt-get install -y sqlite3

EXPOSE 8081

COPY backend/entrypoint.sh .
RUN chmod +x entrypoint.sh

COPY backend/init.sql /app/

ENTRYPOINT ["./entrypoint.sh"]
