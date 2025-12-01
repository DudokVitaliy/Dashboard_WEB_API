# Learn about building .NET container images:
# https://github.com/dotnet/dotnet-docker/blob/main/samples/README.md
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY ["Dashboard_WEB_API/Dashboard_WEB_API.csproj", "Dashboard_WEB_API/"]
COPY ["Dashboard_WEB_API.BLL/Dashboard_WEB_API.BLL.csproj", "Dashboard_WEB_API.BLL/"]
COPY ["Dashboard_WEB_API.DAL/Dashboard_WEB_API.DAL.csproj", "Dashboard_WEB_API.DAL/"]
RUN dotnet restore "Dashboard_WEB_API/Dashboard_WEB_API.csproj"

# copy everything else and build app
COPY . .
WORKDIR /app/Dashboard_WEB_API
RUN dotnet publish -o /app/out


# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Dashboard_WEB_API.dll"]