FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore "FrameworksAndDrivers/Web/DotLanches.Pedido.Api/DotLanches.Pedido.Api.csproj"
RUN dotnet publish "FrameworksAndDrivers/Web/DotLanches.Pedido.Api/DotLanches.Pedido.Api.csproj" -c Release -o out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "DotLanches.Pedido.Api.dll"]
