using Microsoft.EntityFrameworkCore;
using Serilog;
using WebApplication1.Context;

var builder = WebApplication.CreateBuilder(args);

Log.Logger=new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt",rollingInterval:RollingInterval.Minute)
    .Enrich.FromLogContext()
    .CreateBootstrapLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllersWithViews();



const string connectionstring= "Data Source=PTPLL605;" +
          "Initial Catalog=sampledb;" +
          "Integrated Security=True;" +
      "TrustServerCertificate=True;";

builder.Services.AddDbContext<AppDbContext>(x=>x.UseSqlServer(connectionstring));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
