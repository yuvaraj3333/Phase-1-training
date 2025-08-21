using API_Demo.Context;
using API_Demo.Security;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlite(@"Data Source=D:\database.db"));

//var sqlSection = builder.Configuration.GetSection("SQL_CONN");
//string connectionString = sqlSection["connString"];

//builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection("JWT"));

//var jwt = builder.Configuration.GetSection("JWT");
//var issuer = builder.Configuration.GetSection("JWT");
//var audience = 

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
