FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["services/api/src/Gaia.API/Gaia.API.csproj", "services/api/src/Gaia.API/"]
RUN dotnet restore "services/api/src/Gaia.API/Gaia.API.csproj"
COPY . .
WORKDIR "/src/services/api/src/Gaia.API"
RUN dotnet build "Gaia.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gaia.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gaia.API.dll"]