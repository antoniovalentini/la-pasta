FROM mcr.microsoft.com/dotnet/aspnet:7.0 as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

COPY ./src/Dtos /src/Dtos
COPY ./src/Apis /src/Apis

WORKDIR /src/Apis
RUN ls
RUN dotnet build "Apis.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Apis.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Apis.dll"]
