using DotLanches.Pedidos.DataMongo.Repositories;
using DotLanches.Pedidos.DataSql.DatabaseContext;
using DotLanches.Pedidos.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DotLanches.Pedidos.BDDTests.Setup;

public class PedidoApi: WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            SetupInMemoryDatabase(services);
        });
        builder.ConfigureAppConfiguration(cfgBuilder =>
        {
            cfgBuilder.AddEnvironmentVariables();

            var appsettingsFilePath = Path.Combine(Environment.CurrentDirectory, "appsettings.bdd.json");
            cfgBuilder.AddJsonFile(appsettingsFilePath);
        });

        builder.UseEnvironment("Development");
    }

    private static IServiceCollection SetupInMemoryDatabase(IServiceCollection services)
    {
        var dbContextDescriptor = services.Single(
            d => d.ServiceType ==
                 typeof(DbContextOptions<PedidoDbContext>));

        services.Remove(dbContextDescriptor);
        services.AddDbContextPool<PedidoDbContext>(options =>
            options.UseInMemoryDatabase("Pedido"));
        services.AddScoped<IPedidoRepository, PedidoRepository>();

        return services;
    }
}