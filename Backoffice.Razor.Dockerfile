FROM mcr.microsoft.com/dotnet/aspnet:7.0 as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

COPY ./src/Api.Dtos /src/Api.Dtos
COPY ./src/Backoffice.Razor /src/Backoffice.Razor

WORKDIR /src/Backoffice.Razor
RUN ls
RUN dotnet build "Backoffice.Razor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Backoffice.Razor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Backoffice.Razor.dll"]
