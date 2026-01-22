# Usa la imagen de .NET Runtime para ejecutar la app
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Usa el SDK de .NET para compilar
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copia los archivos de proyecto para restaurar dependencias
COPY ["eSaludCareProyect.sln", "./"]
COPY ["CapaDatos/CapaDatos.csproj", "CapaDatos/"]
COPY ["CapaNegocio/CapaNegocio.csproj", "CapaNegocio/"]
COPY ["CapaEntidad/CapaEntidad.csproj", "CapaEntidad/"]
COPY ["CapaComun/CapaComun.csproj", "CapaComun/"]
# Agrega aquí la capa que sea tu Interfaz de Usuario (UI) o API
COPY ["eSaludCareUsers/eSaludCareUsers.csproj", "eSaludCareUsers/"] 

RUN dotnet restore "eSaludCareProyect.sln"

# Copia todo el código y compila
COPY . .
WORKDIR "/src/."
RUN dotnet build "eSaludCareProyect.sln" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eSaludCareProyect.sln" -c Release -o /app/publish

# Configuración final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# REEMPLAZA 'eSaludCareUsers.dll' por el nombre real de tu ejecutable
ENTRYPOINT ["dotnet", "eSaludCareUsers.dll"]

# ... (lo anterior se queda igual)
COPY ["CapaEntidad/CapaEntidad.csproj", "CapaEntidad/"]
COPY ["CapaComun/CapaComun.csproj", "CapaComun/"]
COPY ["eSaludCareUsers/eSaludCareUsers.csproj", "eSaludCareUsers/"] 

# ESTA ES LA LÍNEA QUE FALTA:
COPY ["eSaludCareAdmin/eSaludCareAdmin.csproj", "eSaludCareAdmin/"] 

RUN dotnet restore "eSaludCareProyect.sln"
# ... (el resto se queda igual)