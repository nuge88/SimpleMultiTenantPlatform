# Use the official ASP.NET Core runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image for building the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore any dependencies
COPY ["DocumentService.API/DocumentService.API.csproj", "DocumentService.API/"]
COPY ["DocumentService.Infrastructure/DocumentService.Infrastructure.csproj", "DocumentService.Infrastructure/"]
COPY ["DocumentService.Domain/DocumentService.Domain.csproj", "DocumentService.Domain/"]

# Copy the shared projects as well
COPY ["Shared/Shared.Contracts/Shared.Contracts.csproj", "Shared/Shared.Contracts/"]
COPY ["Shared/Shared.Exceptions/Shared.Exceptions.csproj", "Shared/Shared.Exceptions/"]

RUN dotnet restore "DocumentService.API/DocumentService.API.csproj"

# Copy the rest of the project files and build
COPY . .
WORKDIR "/src/DocumentService.API"
RUN dotnet build "DocumentService.API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "DocumentService.API.csproj" -c Release -o /app/publish

# Use the runtime image for running the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DocumentService.API.dll"]
