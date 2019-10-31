FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 5200

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/Services/Identity/MsSystem.Identity/MsSystem.Identity.csproj", "src/Services/Identity/MsSystem.Identity/"]
COPY ["src/BuildingBlocks/Zipkin/JadeFramework.Zipkin/JadeFramework.Zipkin.csproj", "src/BuildingBlocks/Zipkin/JadeFramework.Zipkin/"]
COPY ["src/BuildingBlocks/Resilience/Resilience.Http.DependencyInjection/Resilience.Http.DependencyInjection.csproj", "src/BuildingBlocks/Resilience/Resilience.Http.DependencyInjection/"]
COPY ["src/BuildingBlocks/Resilience/Resilience.Http/Resilience.Http.csproj", "src/BuildingBlocks/Resilience/Resilience.Http/"]
COPY ["src/BuildingBlocks/Consul/JadeFramework.ServiceRegistration/JadeFramework.ServiceRegistration.csproj", "src/BuildingBlocks/Consul/JadeFramework.ServiceRegistration/"]
RUN dotnet restore "src/Services/Identity/MsSystem.Identity/MsSystem.Identity.csproj"
COPY . .
WORKDIR "/src/src/Services/Identity/MsSystem.Identity"
RUN dotnet build "MsSystem.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MsSystem.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MsSystem.Identity.dll"]