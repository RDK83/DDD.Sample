# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
# Create non-root user
RUN useradd -m -d /app appuser
USER appuser
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/API.Host/API.Host.csproj", "src/API.Host/"]
COPY ["src/API.Models/API.Models.csproj", "src/API.Models/"]
COPY ["src/ValidationRules/ValidationRules.csproj", "src/ValidationRules/"]
COPY ["src/API.Shared/API.Shared.csproj", "src/API.Shared/"]
COPY ["src/Catalogue.Application/Catalogue.Application.csproj", "src/Catalogue.Application/"]
COPY ["src/Catalogue.Domain/Catalogue.Domain.csproj", "src/Catalogue.Domain/"]
COPY ["src/Shared.Domain/Shared.Domain.csproj", "src/Shared.Domain/"]
COPY ["src/SharedKernel/SharedKernel.csproj", "src/SharedKernel/"]
COPY ["src/Shared.Application/Shared.Application.csproj", "src/Shared.Application/"]
COPY ["src/Catalogue.Infrastructure/Catalogue.Infrastructure.csproj", "src/Catalogue.Infrastructure/"]
RUN dotnet restore "./src/API.Host/API.Host.csproj"
COPY . .
WORKDIR "/src/src/API.Host"
RUN dotnet build "./API.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./API.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.Host.dll"]