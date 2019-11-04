FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 8000

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/Web/MVC/MsSystem.Web/MsSystem.Web.csproj", "src/Web/MVC/MsSystem.Web/"]
COPY ["src/Web/MVC/Controllers/MsSystem.Web.Areas.Sys/MsSystem.Web.Areas.Sys.csproj", "src/Web/MVC/Controllers/MsSystem.Web.Areas.Sys/"]
COPY ["src/Web/MVC/MsSystem.Utility/MsSystem.Utility.csproj", "src/Web/MVC/MsSystem.Utility/"]
COPY ["src/Web/MVC/Controllers/MsSystem.Web.Areas.Weixin/MsSystem.Web.Areas.Weixin.csproj", "src/Web/MVC/Controllers/MsSystem.Web.Areas.Weixin/"]
COPY ["src/Web/MVC/Controllers/MsSystem.Web.Areas.OA/MsSystem.Web.Areas.OA.csproj", "src/Web/MVC/Controllers/MsSystem.Web.Areas.OA/"]
COPY ["src/Web/MVC/Controllers/MsSystem.Web.Areas.WF/MsSystem.Web.Areas.WF.csproj", "src/Web/MVC/Controllers/MsSystem.Web.Areas.WF/"]
RUN dotnet restore "src/Web/MVC/MsSystem.Web/MsSystem.Web.csproj"
COPY . .
WORKDIR "/src/src/Web/MVC/MsSystem.Web"
RUN dotnet build "MsSystem.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MsSystem.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MsSystem.Web.dll"]