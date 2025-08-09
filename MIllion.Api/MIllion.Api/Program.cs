using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Million.Application.Mappings;
using Million.Application.Services.Implementations;
using Million.Application.Services.Interfaces;
using Million.Data.Entities;
using Million.Data.Interfaces;
using Million.Infrastructure.Mongo;
using Million.Infrastructure.Repository.Implementations;
using MongoDB.Driver;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.


builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection(nameof(MongoDbSettings)));

builder.Services.AddScoped<IRepository<Property>, MongoRepository<Property>>();
builder.Services.AddScoped<IPropertyService, PropertyService>();

builder.Services.AddAutoMapper(typeof(PropertyProfile));


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
