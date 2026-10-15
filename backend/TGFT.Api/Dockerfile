# Contexte de build : racine du dépôt (voir docker-compose.yml).

# --- Étape 1 : compilation ---
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Les .csproj d'abord : la restauration NuGet reste en cache tant qu'ils ne changent pas.
COPY backend/TGFT.Domain/TGFT.Domain.csproj backend/TGFT.Domain/
COPY backend/TGFT.Application/TGFT.Application.csproj backend/TGFT.Application/
COPY backend/TGFT.Infrastructure/TGFT.Infrastructure.csproj backend/TGFT.Infrastructure/
COPY backend/TGFT.Api/TGFT.Api.csproj backend/TGFT.Api/
RUN dotnet restore backend/TGFT.Api/TGFT.Api.csproj

COPY backend/ backend/
RUN dotnet publish backend/TGFT.Api/TGFT.Api.csproj -c Release -o /app --no-restore

# --- Étape 2 : exécution (runtime seul, sans SDK) ---
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app .

# Utilisateur non-root fourni par l'image Microsoft ; écoute sur 8080 par défaut.
USER $APP_UID
EXPOSE 8080
ENTRYPOINT ["dotnet", "TGFT.Api.dll"]
