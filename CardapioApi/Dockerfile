# Base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8086
EXPOSE 8087

# Define com ambiente dev
ENV ASPNETCORE_ENVIRONMENT=Development

# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos .csproj primeiro para aproveitar o cache de camadas do Docker
COPY ["CardapioApi/CardapioApi.csproj", "API/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain.csproj", "Domain/"]

RUN dotnet restore "CardapioApi/CardapioApi.csproj"

# Copia o restante dos arquivos
COPY . .
WORKDIR "src/API"
RUN dotnet publish -c Release -o /app/publish  

# Runtime
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish . 
ENTRYPOINT ["dotnet", "CardapioApi.dll"]