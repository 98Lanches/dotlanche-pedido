using DotLanches.Pedidos.Api.Extensions;
using Serilog;
using System.Text.Json.Serialization;

namespace DotLanches.Pedidos.Api;

public class Program
{
    public static WebApplication CreateApp(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.ConfigureApplicationServices(builder.Configuration);
        builder.Services.ConfigureLogging(builder.Configuration);
        builder.Services.ConfigureOpenTelemetry(builder.Configuration);

        builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        var app = builder.Build();

        app.UseSerilogRequestLogging();
        app.MapHealthChecks("/health");
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapControllers();
        app.UseExceptionHandler();

        return app;
    }

    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        try
        {
            var app = CreateApp(args);
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}