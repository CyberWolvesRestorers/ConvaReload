FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ConvaReload/ConvaReload.csproj", "ConvaReload/"]
RUN dotnet restore "ConvaReload/ConvaReload.csproj"
COPY . .
WORKDIR "/src/ConvaReload"
RUN dotnet build "ConvaReload.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConvaReload.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConvaReload.dll"]