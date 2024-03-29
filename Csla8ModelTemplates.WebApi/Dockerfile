#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Csla8ModelTemplates.WebApi/Csla8ModelTemplates.WebApi.csproj", "Csla8ModelTemplates.WebApi/"]
COPY ["Csla8ModelTemplates.Dal.MySql/Csla8ModelTemplates.Dal.MySql.csproj", "Csla8ModelTemplates.Dal.MySql/"]
COPY ["Csla8ModelTemplates.Contracts/Csla8ModelTemplates.Contracts.csproj", "Csla8ModelTemplates.Contracts/"]
COPY ["Csla8RestApi.Dal/Csla8RestApi.Dal.csproj", "Csla8RestApi.Dal/"]
COPY ["Csla8ModelTemplates.Entities/Csla8ModelTemplates.Entities.csproj", "Csla8ModelTemplates.Entities/"]
COPY ["Csla8ModelTemplates.Resources/Csla8ModelTemplates.Resources.csproj", "Csla8ModelTemplates.Resources/"]
COPY ["Csla8ModelTemplates.Models/Csla8ModelTemplates.Models.csproj", "Csla8ModelTemplates.Models/"]
COPY ["Csla8RestApi.Models/Csla8RestApi.Models.csproj", "Csla8RestApi.Models/"]
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