FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 5002

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/Services/System/MsSystem.Sys.API/MsSystem.Sys.API.csproj", "src/Services/System/MsSystem.Sys.API/"]
COPY ["src/Services/System/MsSystem.Sys.IService/MsSystem.Sys.IService.csproj", "src/Services/System/MsSystem.Sys.IService/"]
COPY ["src/Services/System/MsSystem.Sys.Model/MsSystem.Sys.Model.csproj", "src/Services/System/MsSystem.Sys.Model/"]
COPY ["src/Services/System/MsSystem.Sys.ViewModel/MsSystem.Sys.ViewModel.csproj", "src/Services/System/MsSystem.Sys.ViewModel/"]
COPY ["src/Services/System/MsSystem.Sys.Schedule.Infrastructure/MsSystem.Sys.Schedule.Infrastructure.csproj", "src/Services/System/MsSystem.Sys.Schedule.Infrastructure/"]
COPY ["src/Services/System/MsSystem.Sys.IRepository/MsSystem.Sys.IRepository.csproj", "src/Services/System/MsSystem.Sys.IRepository/"]
COPY ["src/Services/System/MsSystem.Sys.Service/MsSystem.Sys.Service.csproj", "src/Services/System/MsSystem.Sys.Service/"]
COPY ["src/Services/System/MsSystem.Sys.Repository/MsSystem.Sys.Repository.csproj", "src/Services/System/MsSystem.Sys.Repository/"]
RUN dotnet restore "src/Services/System/MsSystem.Sys.API/MsSystem.Sys.API.csproj"
COPY . .
WORKDIR "/src/src/Services/System/MsSystem.Sys.API"
RUN dotnet build "MsSystem.Sys.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MsSystem.Sys.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MsSystem.Sys.API.dll"]