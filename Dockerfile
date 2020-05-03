FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ./cei-api-academica/Cei.Api.Academica.csproj ./cei-api-academica/
COPY ./cei-api-common/Cei.Api.Common.csproj ./cei-api-common/
RUN dotnet restore "./cei-api-academica/Cei.Api.Academica.csproj"
COPY ./cei-api-academica/* ./cei-api-academica/
COPY ./cei-api-common/* ./cei-api-common/
WORKDIR /src/cei-api-academica
RUN dotnet build "Cei.Api.Academica.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cei.Api.Academica.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cei.Api.Academica.dll"]
