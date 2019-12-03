FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 5004

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/Services/Weixin/MsSystem.Weixin.API/MsSystem.Weixin.API.csproj", "src/Services/Weixin/MsSystem.Weixin.API/"]
COPY ["src/Services/Weixin/MsSystem.Weixin.IRepository/MsSystem.Weixin.IRepository.csproj", "src/Services/Weixin/MsSystem.Weixin.IRepository/"]
COPY ["src/Services/Weixin/MsSystem.Weixin.Model/MsSystem.Weixin.Model.csproj", "src/Services/Weixin/MsSystem.Weixin.Model/"]
COPY ["src/Services/Weixin/MsSystem.Weixin.ViewModel/MsSystem.Weixin.ViewModel.csproj", "src/Services/Weixin/MsSystem.Weixin.ViewModel/"]
COPY ["src/Services/Weixin/MsSystem.Weixin.IService/MsSystem.Weixin.IService.csproj", "src/Services/Weixin/MsSystem.Weixin.IService/"]
COPY ["src/Services/Weixin/MsSystem.Weixin.Repository/MsSystem.Weixin.Repository.csproj", "src/Services/Weixin/MsSystem.Weixin.Repository/"]
COPY ["src/Services/Weixin/MsSystem.Weixin.Service/MsSystem.Weixin.Service.csproj", "src/Services/Weixin/MsSystem.Weixin.Service/"]
RUN dotnet restore "src/Services/Weixin/MsSystem.Weixin.API/MsSystem.Weixin.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Weixin/MsSystem.Weixin.API"
RUN dotnet build "MsSystem.Weixin.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MsSystem.Weixin.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MsSystem.Weixin.API.dll"]