FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet run --project Build/Build.csproj -- publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

WORKDIR /app
COPY --from=build /src/publish .

COPY ["SimpleClinicApi/appsettings.json", "./appsettings.json"]
COPY ["SimpleClinicApi/appsettings.Development.json", "./appsettings.Development.json"]

EXPOSE 8080

ENTRYPOINT ["dotnet", "SimpleClinicApi.dll"]
