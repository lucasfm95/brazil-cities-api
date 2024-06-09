FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BrazilCities.Api/BrazilCities.Api.csproj", "BrazilCities.Api/"]
RUN dotnet restore "BrazilCities.Api/BrazilCities.Api.csproj"
COPY . .
WORKDIR "/src/BrazilCities.Api"
RUN dotnet build "BrazilCities.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BrazilCities.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BrazilCities.Api.dll"]
