# Use the ASP.NET Core runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80 443

# Install OpenSSL to generate a self-signed certificate
RUN apt-get update && apt-get install -y openssl

# Create the necessary directories for the certificate
RUN mkdir /https

# Generate a self-signed SSL certificate using OpenSSL
RUN openssl req -x509 -nodes -days 365 -newkey rsa:2048 \
    -keyout /https/aspnetapp.key \
    -out /https/aspnetapp.crt \
    -subj "/CN=localhost"

# Combine the certificate and key into a .pfx file
RUN openssl pkcs12 -export -out /https/aspnetapp.pfx \
    -inkey /https/aspnetapp.key \
    -in /https/aspnetapp.crt \
    -passout pass:Password1234

# Use the SDK image for building the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the DocumentService project files
COPY ["Services/DocumentService/DocumentService.API/DocumentService.API.csproj", "Services/DocumentService/DocumentService.API/"]
COPY ["Services/DocumentService/DocumentService.Application/DocumentService.Application.csproj", "Services/DocumentService/DocumentService.Application/"]
COPY ["Services/DocumentService/DocumentService.Infrastructure/DocumentService.Infrastructure.csproj", "Services/DocumentService/DocumentService.Infrastructure/"]
COPY ["Services/DocumentService/DocumentService.Domain/DocumentService.Domain.csproj", "Services/DocumentService/DocumentService.Domain/"]

# Copy the Shared projects (adjust the path to go up two directories)
COPY ["Shared/Shared.Contracts/Shared.Contracts.csproj", "Shared/Shared.Contracts/"]
COPY ["Shared/Shared.Exceptions/Shared.Exceptions.csproj", "Shared/Shared.Exceptions/"]

# Restore dependencies for the API project, which will also restore dependencies for the Shared projects
RUN dotnet restore "Services/DocumentService/DocumentService.API/DocumentService.API.csproj"

# Copy the entire solution (including shared projects and service files)
COPY . .

# Build the project
WORKDIR "/src/Services/DocumentService/DocumentService.API"
RUN dotnet build "DocumentService.API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "DocumentService.API.csproj" -c Release -o /app/publish

# Use the runtime image for running the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DocumentService.API.dll"]
