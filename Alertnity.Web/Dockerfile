# Base image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy solution and project files
COPY ["Alertnity.sln", "./"]
COPY ["Alertnity.Web/Alertnity.Web.csproj", "Alertnity.Web/"]
COPY ["Alertnity/Alertnity.csproj", "Alertnity/"]

# Restore dependencies
RUN dotnet restore "Alertnity.sln"

# Copy everything
COPY . .

# Build the project
WORKDIR "/src/Alertnity.Web"
RUN dotnet build "Alertnity.Web.csproj" --configuration ${BUILD_CONFIGURATION} --output /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Alertnity.Web.csproj" --configuration ${BUILD_CONFIGURATION} --output /app/publish /p:UseAppHost=false

# Final runtime stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Alertnity.Web.dll"]
