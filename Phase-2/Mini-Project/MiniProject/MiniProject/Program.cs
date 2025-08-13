using Microsoft.EntityFrameworkCore;
using MovieBookingApp.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add services
builder.Services.AddControllersWithViews();

// ✅ Add DbContext
var connectionString = "Data Source=PTSQLTESTDB01; Initial Catalog=Yuvaraj; Integrated Security=True; TrustServerCertificate=True;";

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString));

var app = builder.Build();

// ✅ Use static files (wwwroot)
app.UseStaticFiles();

// ✅ Use routing and error handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Optional error page
    app.UseHsts();
}

// ✅ Use routing
app.UseRouting();

// ✅ Map default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
