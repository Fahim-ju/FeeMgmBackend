FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FeeMgmBackend/FeeMgmBackend.csproj", "FeeMgmBackend/"]
RUN dotnet restore "FeeMgmBackend/FeeMgmBackend.csproj"
COPY . .
WORKDIR "/src/FeeMgmBackend"
RUN dotnet build "FeeMgmBackend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FeeMgmBackend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FeeMgmBackend.dll"]
