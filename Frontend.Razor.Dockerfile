FROM mcr.microsoft.com/dotnet/aspnet:7.0 as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

COPY ./src/Api.Dtos /src/Api.Dtos
COPY ./src/Frontend.Razor /src/Frontend.Razor
COPY *.props /

WORKDIR /src/Frontend.Razor
RUN ls
RUN dotnet build "Frontend.Razor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Frontend.Razor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Frontend.Razor.dll"]
