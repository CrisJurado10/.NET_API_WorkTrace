# Etapa 1: Construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar el archivo de solución y los proyectos para restaurar dependencias
COPY ["WorkTraceBackendApi.sln", "./"]
COPY ["WorkTrace.Api/WorkTrace.Api.csproj", "WorkTrace.Api/"]
COPY ["WorkTrace.Application/WorkTrace.Application.csproj", "WorkTrace.Application/"]
COPY ["WorkTrace.Data/WorkTrace.Data.csproj", "WorkTrace.Data/"]
COPY ["WorkTrace.Logic/WorkTrace.Logic.csproj", "WorkTrace.Logic/"]
COPY ["WorkTrace.Repositories/WorkTrace.Repositories.csproj", "WorkTrace.Repositories/"]

# Restaurar dependencias
RUN dotnet restore "WorkTraceBackendApi.sln"

# Copiar el resto del código
COPY . .

# Construir y publicar la API
WORKDIR "/src/WorkTrace.Api"
RUN dotnet publish "WorkTrace.Api.csproj" -c Release -o /app/publish

# Etapa 2: Ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Configurar el puerto para que Render lo detecte
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "WorkTrace.Api.dll"]