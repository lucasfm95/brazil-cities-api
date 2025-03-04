using System.Text.Json.Serialization;
using BrazilCities.Api.Configurations;
using BrazilCities.Persistence.Context;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks()
    .AddNpgSql(Environment.GetEnvironmentVariable("CONNECTION_STRING_DB_POSTGRES") ?? 
               throw new Exception("CONNECTION_STRING_DB_POSTGRES not found"));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddServices();
builder.Services.AddDbContext<AppDbContext>( contextLifetime: ServiceLifetime.Scoped, optionsLifetime: ServiceLifetime.Scoped);
builder.Services.AddRepositories();
builder.Services.AddOpenApi();

var app = builder.Build();
app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "OpenAPI v1");
});
app.MapScalarApiReference();
app.UseHttpsRedirection();
app.UseCors(policyBuilder => 
    policyBuilder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());   
app.UseAuthorization();
app.MapControllers();
app.UseHealthcheck();
app.Run();