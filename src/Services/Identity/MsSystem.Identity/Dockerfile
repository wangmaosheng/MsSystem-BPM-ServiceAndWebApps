FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5200

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/Services/Identity/MsSystem.Identity/MsSystem.Identity.csproj", "src/Services/Identity/MsSystem.Identity/"]
RUN dotnet restore "src/Services/Identity/MsSystem.Identity/MsSystem.Identity.csproj"
COPY . .
WORKDIR "/src/src/Services/Identity/MsSystem.Identity"
RUN dotnet build "MsSystem.Identity.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MsSystem.Identity.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MsSystem.Identity.dll"]