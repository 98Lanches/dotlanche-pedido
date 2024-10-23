using DotLanches.Api.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureApplicationServices(builder.Configuration);

builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddHttpLogging(logging =>
{
});

var app = builder.Build();

app.MapHealthChecks("/health");

app.UseHttpLogging();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.UseExceptionHandler();

app.Run();