FROM microsoft/dotnet:sdk as build-env
WORKDIR /code
COPY *.csproj /code
RUN dotnet restore
COPY . /code
RUN dotnet publish -c Release -o out
FROM microsoft/dotnet:runtime
WORKDIR /app
COPY --from=build-env /code/out /app
ENTRYPOINT [ "dotnet","MsSystem.Identity" ]