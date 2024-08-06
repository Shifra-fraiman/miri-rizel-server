# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files and restore
COPY ["CopyRight.WebApi/CopyRight.WebApi.csproj", "CopyRight.WebApi/"]
COPY ["CopyRight.Bl/CopyRight.Bl.csproj", "CopyRight.Bl/"]
COPY ["CopyRight.Dto/CopyRight.Dto.csproj", "CopyRight.Dto/"]
COPY ["CopyRight.Dal/CopyRight.Dal.csproj", "CopyRight.Dal/"]
RUN dotnet restore "./CopyRight.WebApi/CopyRight.WebApi.csproj"

# Copy the rest of the files and build
COPY . .
WORKDIR "/src/CopyRight.WebApi"
RUN dotnet build "./CopyRight.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 2: Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CopyRight.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 3: Final Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Copy published application
COPY --from=publish /app/publish .

# Add credentials file
COPY CopyRight.WebApi/Resources/credentials.json /app/Resources/credentials.json

ENTRYPOINT ["dotnet", "CopyRight.WebApi.dll"]
