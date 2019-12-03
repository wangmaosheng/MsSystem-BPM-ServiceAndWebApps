FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 5200

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/Services/Identity/MsSystem.Identity/MsSystem.Identity.csproj", "src/Services/Identity/MsSystem.Identity/"]
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