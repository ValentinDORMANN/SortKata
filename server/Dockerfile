# Utiliser l'image de base ASP.NET pour exécuter l'application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Etape de construction avec le SDK .NET
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copier les fichiers de projet et restaurer les dépendances
COPY ["SortKata/SortKata.csproj", "SortKata/"]
COPY ["SortKataTest/SortKataTest.csproj", "SortKataTest/"]
RUN dotnet restore "./SortKata/SortKata.csproj"
RUN dotnet restore "./SortKataTest/SortKataTest.csproj"

# Copier tout le code source
COPY . .

# Construire le projet principal
WORKDIR "/src/SortKata"
RUN dotnet build "./SortKata.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Construire et exécuter les tests unitaires
WORKDIR "/src/SortKataTest"
RUN dotnet build "./SortKataTest.csproj" -c $BUILD_CONFIGURATION
RUN dotnet test "./SortKataTest.csproj" --no-restore --logger "trx;LogFileName=test_results.trx"

# Publier le projet principal
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR "/src/SortKata"
RUN dotnet publish "./SortKata.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Image finale pour exécuter l'application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SortKata.dll"]
