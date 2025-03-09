using System.Text.Json.Serialization;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using BrazilCities.Api.Configurations;
using BrazilCities.Persistence.Context;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks()
    .AddNpgSql(Environment.GetEnvironmentVariable("CONNECTION_STRING_DB_POSTGRES") ?? 
               throw new Exception("CONNECTION_STRING_DB_POSTGRES not found"));

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddMvc().AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
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
    IReadOnlyCollection<ApiVersionDescription> descriptions = app.DescribeApiVersions();
    
    foreach (ApiVersionDescription description in descriptions)
    {
        options.SwaggerEndpoint($"/openapi/v{description.ApiVersion.MajorVersion}.json", $"OpenAPI v{description.ApiVersion.MajorVersion}");
    }
});
app.MapScalarApiReference();
app.UseHttpsRedirection();
app.UseCors(policyBuilder => policyBuilder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());   
app.UseAuthorization();
app.MapControllers();
app.UseHealthcheck();
app.Run();