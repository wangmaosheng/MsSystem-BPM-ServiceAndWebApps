FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 5003

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/Services/WorkFlow/MsSystem.WF.API/MsSystem.WF.API.csproj", "src/Services/WorkFlow/MsSystem.WF.API/"]
COPY ["src/Services/WorkFlow/MsSystem.WF.Repository/MsSystem.WF.Repository.csproj", "src/Services/WorkFlow/MsSystem.WF.Repository/"]
COPY ["src/Services/WorkFlow/MsSystem.WF.Model/MsSystem.WF.Model.csproj", "src/Services/WorkFlow/MsSystem.WF.Model/"]
COPY ["src/Services/WorkFlow/MsSystem.WF.ViewModel/MsSystem.WF.ViewModel.csproj", "src/Services/WorkFlow/MsSystem.WF.ViewModel/"]
COPY ["src/Services/WorkFlow/MsSystem.WF.IRepository/MsSystem.WF.IRepository.csproj", "src/Services/WorkFlow/MsSystem.WF.IRepository/"]
COPY ["src/Services/WorkFlow/MsSystem.WF.Service/MsSystem.WF.Service.csproj", "src/Services/WorkFlow/MsSystem.WF.Service/"]
COPY ["src/Services/WorkFlow/MsSystem.WF.IService/MsSystem.WF.IService.csproj", "src/Services/WorkFlow/MsSystem.WF.IService/"]
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