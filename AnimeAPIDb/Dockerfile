FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AnimeAPIDb/AnimeAPIDb.csproj", "AnimeAPIDb/"]
RUN dotnet restore "AnimeAPIDb/AnimeAPIDb.csproj"
COPY . .
WORKDIR "/src/AnimeAPIDb"
RUN dotnet build "AnimeAPIDb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AnimeAPIDb.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AnimeAPIDb.dll"]
