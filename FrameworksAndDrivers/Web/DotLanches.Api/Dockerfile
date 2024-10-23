FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore "FrameworksAndDrivers/Web/DotLanches.Api/DotLanches.Api.csproj"
RUN dotnet publish "FrameworksAndDrivers/Web/DotLanches.Api/DotLanches.Api.csproj" -c Release -o out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "DotLanches.Api.dll"]
