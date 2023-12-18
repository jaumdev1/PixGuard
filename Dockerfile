FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY PixGuard.Api/PixGuard.Api.csproj PixGuard.Api/
COPY PixGuard.Domain/PixGuard.Domain.csproj PixGuard.Domain/

# Restaurar as dependências
RUN dotnet restore PixGuard.Api/PixGuard.Api.csproj

# Copiar o resto do código
COPY . .

WORKDIR "/src/PixGuard.Api"
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR "/src/PixGuard.Api"
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PixGuard.Api.dll"]
