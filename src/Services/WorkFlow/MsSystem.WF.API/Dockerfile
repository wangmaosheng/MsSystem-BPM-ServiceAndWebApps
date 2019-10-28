FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 5003

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/Services/WorkFlow/MsSystem.WF.API/MsSystem.WF.API.csproj", "src/Services/WorkFlow/MsSystem.WF.API/"]
COPY ["src/Services/WorkFlow/MsSystem.WF.Repository/MsSystem.WF.Repository.csproj", "src/Services/WorkFlow/MsSystem.WF.Repository/"]
COPY ["src/Services/WorkFlow/MsSystem.WF.Model/MsSystem.WF.Model.csproj", "src/Services/WorkFlow/MsSystem.WF.Model/"]
COPY ["src/Services/WorkFlow/MsSystem.WF.ViewModel/MsSystem.WF.ViewModel.csproj", "src/Services/WorkFlow/MsSystem.WF.ViewModel/"]
COPY ["src/Services/WorkFlow/MsSystem.WF.IRepository/MsSystem.WF.IRepository.csproj", "src/Services/WorkFlow/MsSystem.WF.IRepository/"]
COPY ["src/Services/WorkFlow/MsSystem.WF.Service/MsSystem.WF.Service.csproj", "src/Services/WorkFlow/MsSystem.WF.Service/"]
COPY ["src/Services/WorkFlow/MsSystem.WF.IService/MsSystem.WF.IService.csproj", "src/Services/WorkFlow/MsSystem.WF.IService/"]
COPY ["src/BuildingBlocks/Zipkin/JadeFramework.Zipkin/JadeFramework.Zipkin.csproj", "src/BuildingBlocks/Zipkin/JadeFramework.Zipkin/"]
COPY ["src/BuildingBlocks/Resilience/Resilience.Http.DependencyInjection/Resilience.Http.DependencyInjection.csproj", "src/BuildingBlocks/Resilience/Resilience.Http.DependencyInjection/"]
COPY ["src/BuildingBlocks/Resilience/Resilience.Http/Resilience.Http.csproj", "src/BuildingBlocks/Resilience/Resilience.Http/"]
COPY ["src/BuildingBlocks/Consul/JadeFramework.ServiceRegistration/JadeFramework.ServiceRegistration.csproj", "src/BuildingBlocks/Consul/JadeFramework.ServiceRegistration/"]
RUN dotnet restore "src/Services/WorkFlow/MsSystem.WF.API/MsSystem.WF.API.csproj"
COPY . .
WORKDIR "/src/src/Services/WorkFlow/MsSystem.WF.API"
RUN dotnet build "MsSystem.WF.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MsSystem.WF.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MsSystem.WF.API.dll"]