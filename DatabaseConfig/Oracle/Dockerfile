#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Csla8RestApi/Csla8RestApi.csproj", "Csla8RestApi/"]
COPY ["Csla8ModelTemplates.Contracts/Csla8ModelTemplates.Contracts.csproj", "Csla8ModelTemplates.Contracts/"]
COPY ["Csla8ModelTemplates.Dal/Csla8ModelTemplates.Dal.csproj", "Csla8ModelTemplates.Dal/"]
COPY ["Csla8ModelTemplates.Entities/Csla8ModelTemplates.Entities.csproj", "Csla8ModelTemplates.Entities/"]
COPY ["Csla8ModelTemplates.Dal.Oracle/Csla8ModelTemplates.Dal.Oracle.csproj", "Csla8ModelTemplates.Dal.Oracle/"]
COPY ["Csla8ModelTemplates.Models/Csla8ModelTemplates.Models.csproj", "Csla8ModelTemplates.Models/"]
COPY ["Csla8ModelTemplates.WebApi/Csla8ModelTemplates.WebApi.csproj", "Csla8ModelTemplates.WebApi/"]
RUN dotnet restore "./Csla8ModelTemplates.WebApi/Csla8ModelTemplates.WebApi.csproj"
COPY . .
WORKDIR "/src/Csla8ModelTemplates.WebApi"
RUN dotnet build "./Csla8ModelTemplates.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Csla8ModelTemplates.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Csla8ModelTemplates.WebApi.dll"]
