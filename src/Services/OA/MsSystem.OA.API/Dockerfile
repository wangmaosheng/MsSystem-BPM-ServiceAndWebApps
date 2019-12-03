FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 5006

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/Services/OA/MsSystem.OA.API/MsSystem.OA.API.csproj", "src/Services/OA/MsSystem.OA.API/"]
COPY ["src/Services/OA/MsSystem.OA.IService/MsSystem.OA.IService.csproj", "src/Services/OA/MsSystem.OA.IService/"]
COPY ["src/Services/OA/MsSystem.OA.ViewModel/MsSystem.OA.ViewModel.csproj", "src/Services/OA/MsSystem.OA.ViewModel/"]
COPY ["src/Services/OA/MsSystem.OA.Model/MsSystem.OA.Model.csproj", "src/Services/OA/MsSystem.OA.Model/"]
COPY ["src/Services/OA/MsSystem.OA.Service/MsSystem.OA.Service.csproj", "src/Services/OA/MsSystem.OA.Service/"]
COPY ["src/Services/OA/MsSystem.OA.IRepository/MsSystem.OA.IRepository.csproj", "src/Services/OA/MsSystem.OA.IRepository/"]
COPY ["src/Services/OA/MsSystem.OA.Repository/MsSystem.OA.Repository.csproj", "src/Services/OA/MsSystem.OA.Repository/"]
RUN dotnet restore "src/Services/OA/MsSystem.OA.API/MsSystem.OA.API.csproj"
COPY . .
WORKDIR "/src/src/Services/OA/MsSystem.OA.API"
RUN dotnet build "MsSystem.OA.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MsSystem.OA.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MsSystem.OA.API.dll"]