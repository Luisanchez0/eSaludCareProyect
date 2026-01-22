# 1. Etapa de ejecución (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# 2. Etapa de compilación (SDK)
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copiamos el archivo de solución
COPY ["eSaludCareProyect.sln", "./"]

# Copiamos TODOS los archivos de proyecto (.csproj) de cada carpeta
COPY ["CapaDatos/CapaDatos.csproj", "CapaDatos/"]
COPY ["CapaNegocio/CapaNegocio.csproj", "CapaNegocio/"]
COPY ["CapaEntidad/CapaEntidad.csproj", "CapaEntidad/"]
COPY ["CapaComun/CapaComun.csproj", "CapaComun/"]
COPY ["eSaludCareUsers/eSaludCareUsers.csproj", "eSaludCareUsers/"]
COPY ["eSaludCareAdmin/eSaludCareAdmin.csproj", "eSaludCareAdmin/"]
COPY ["Apis/Apis.csproj", "Apis/"]

# Restauramos las dependencias de toda la solución
RUN dotnet restore "eSaludCareProyect.sln"

# Copiamos el resto del código fuente de todas las carpetas
COPY . .

# Compilamos el proyecto
RUN dotnet build "eSaludCareProyect.sln" -c Release -o /app/build

# 3. Etapa de publicación
FROM build AS publish
RUN dotnet publish "eSaludCareProyect.sln" -c Release -o /app/publish

# 4. Configuración final para Docker Desktop
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# IMPORTANTE: Asegúrate de que eSaludCareUsers.dll sea tu proyecto de inicio
ENTRYPOINT ["dotnet", "eSaludCareUsers.dll"]