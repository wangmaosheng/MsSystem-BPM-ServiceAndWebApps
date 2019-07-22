FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/ApiGateways/MsSystem.Gateway/MsSystem.Gateway.csproj", "src/ApiGateways/MsSystem.Gateway/"]
RUN dotnet restore "src/ApiGateways/MsSystem.Gateway/MsSystem.Gateway.csproj"
COPY . .
WORKDIR "/src/src/ApiGateways/MsSystem.Gateway"
RUN dotnet build "MsSystem.Gateway.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MsSystem.Gateway.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MsSystem.Gateway.dll"]