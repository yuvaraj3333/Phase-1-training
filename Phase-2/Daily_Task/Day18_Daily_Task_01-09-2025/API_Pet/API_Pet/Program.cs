using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using API_Pet.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Sql connection string from appsettings.json
builder.Services.AddDbContext<AppDBContext>(o => o.UseSqlite(builder.Configuration.GetConnectionString("Sql")));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // needed for Swagger UI
builder.Services.AddSwaggerGen();           // adds Swagger generation

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();       // enables JSON endpoint
    app.UseSwaggerUI();     // enables Swagger UI at /swagger
}

app.UseAuthorization();

app.MapControllers();

app.Run();