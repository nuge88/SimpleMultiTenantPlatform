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

# Copy the OrganizationService project files
COPY ["Services/OrganizationService/OrganizationService.API/OrganizationService.API.csproj", "Services/OrganizationService/OrganizationService.API/"]
COPY ["Services/OrganizationService/OrganizationService.Application/OrganizationService.Application.csproj", "Services/OrganizationService/OrganizationService.Application/"]
COPY ["Services/OrganizationService/OrganizationService.Infrastructure/OrganizationService.Infrastructure.csproj", "Services/OrganizationService/OrganizationService.Infrastructure/"]
COPY ["Services/OrganizationService/OrganizationService.Domain/OrganizationService.Domain.csproj", "Services/OrganizationService/OrganizationService.Domain/"]

# Copy the Shared projects (adjust the path to go up two directories)
COPY ["Shared/Shared.Contracts/Shared.Contracts.csproj", "Shared/Shared.Contracts/"]
COPY ["Shared/Shared.Exceptions/Shared.Exceptions.csproj", "Shared/Shared.Exceptions/"]

# Restore dependencies for the API project, which will also restore dependencies for the Shared projects
RUN dotnet restore "Services/OrganizationService/OrganizationService.API/OrganizationService.API.csproj"

# Copy the entire solution (including shared projects and service files)
COPY . .

# Build the project
WORKDIR "/src/Services/OrganizationService/OrganizationService.API"
RUN dotnet build "OrganizationService.API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "OrganizationService.API.csproj" -c Release -o /app/publish

# Use the runtime image for running the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrganizationService.API.dll"]
