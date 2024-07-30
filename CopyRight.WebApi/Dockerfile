#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CopyRight.WebApi/CopyRight.WebApi.csproj", "CopyRight.WebApi/"]
COPY ["CopyRight.Bl/CopyRight.Bl.csproj", "CopyRight.Bl/"]
COPY ["CopyRight.Dto/CopyRight.Dto.csproj", "CopyRight.Dto/"]
COPY ["CopyRight.Dal/CopyRight.Dal.csproj", "CopyRight.Dal/"]
RUN dotnet restore "./CopyRight.WebApi/CopyRight.WebApi.csproj"
COPY . .
WORKDIR "/src/CopyRight.WebApi"
RUN dotnet build "./CopyRight.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CopyRight.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CopyRight.WebApi.dll"]