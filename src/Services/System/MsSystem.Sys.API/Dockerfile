FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5002

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/Services/System/MsSystem.Sys.API/MsSystem.Sys.API.csproj", "src/Services/System/MsSystem.Sys.API/"]
COPY ["src/Services/System/MsSystem.Sys.IService/MsSystem.Sys.IService.csproj", "src/Services/System/MsSystem.Sys.IService/"]
COPY ["src/Services/System/MsSystem.Sys.Model/MsSystem.Sys.Model.csproj", "src/Services/System/MsSystem.Sys.Model/"]
COPY ["src/Services/System/MsSystem.Sys.ViewModel/MsSystem.Sys.ViewModel.csproj", "src/Services/System/MsSystem.Sys.ViewModel/"]
COPY ["src/Services/System/MsSystem.Sys.IRepository/MsSystem.Sys.IRepository.csproj", "src/Services/System/MsSystem.Sys.IRepository/"]
COPY ["src/Services/System/MsSystem.Sys.Service/MsSystem.Sys.Service.csproj", "src/Services/System/MsSystem.Sys.Service/"]
COPY ["src/Services/System/MsSystem.Sys.Repository/MsSystem.Sys.Repository.csproj", "src/Services/System/MsSystem.Sys.Repository/"]
COPY ["src/BuildingBlocks/Zipkin/JadeFramework.Zipkin/JadeFramework.Zipkin.csproj", "src/BuildingBlocks/Zipkin/JadeFramework.Zipkin/"]
COPY ["src/BuildingBlocks/Resilience/Resilience.Http.DependencyInjection/Resilience.Http.DependencyInjection.csproj", "src/BuildingBlocks/Resilience/Resilience.Http.DependencyInjection/"]
COPY ["src/BuildingBlocks/Resilience/Resilience.Http/Resilience.Http.csproj", "src/BuildingBlocks/Resilience/Resilience.Http/"]
RUN dotnet restore "src/Services/System/MsSystem.Sys.API/MsSystem.Sys.API.csproj"
COPY . .
WORKDIR "/src/src/Services/System/MsSystem.Sys.API"
RUN dotnet build "MsSystem.Sys.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MsSystem.Sys.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MsSystem.Sys.API.dll"]