FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore "src/FrameworksAndDrivers/Web/DotLanches.Pedidos.Api/\
DotLanches.Pedidos.Api.csproj" && \
    dotnet publish "src/FrameworksAndDrivers/Web/DotLanches.Pedidos.Api/\
DotLanches.Pedidos.Api.csproj" -c Release -o out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "DotLanches.Pedidos.Api.dll"]
