FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/ApiGateways/MsSystem.Gateway/MsSystem.Gateway.csproj", "src/ApiGateways/MsSystem.Gateway/"]
RUN dotnet restore "src/ApiGateways/MsSystem.Gateway/MsSystem.Gateway.csproj"
COPY . .
WORKDIR "/src/src/ApiGateways/MsSystem.Gateway"
RUN dotnet build "MsSystem.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MsSystem.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MsSystem.Gateway.dll"]