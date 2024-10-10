using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using bumbo.Data;  // Ensure the namespace matches your BumboDBContext file
using bumbo.Models;  // Ensure the namespace matches your Employee model

using bumbo.Interfaces;
using bumbo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BumboDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("bumbo")));

builder.Services.AddIdentity<Employee, IdentityRole>()
    .AddEntityFrameworkStores<BumboDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

//services test Douwe
builder.Services.AddScoped<IWeekOverviewService, WeekOverviewService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Routing for the default pages
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Custom routes for specific pages
app.MapControllerRoute(
    name: "test",
    pattern: "{controller=Test}/{action=LoginAsJohnDoe}");

app.MapControllerRoute(
    name: "prognoses",
    pattern: "prognoses",
    defaults: new { controller = "Forecasts", action = "Index" });

app.MapControllerRoute(
    name: "normeringen",
    pattern: "normeringen",
    defaults: new { controller = "Norms", action = "Index" });

app.MapControllerRoute(
    name: "medewerkers",
    pattern: "medewerkers",
    defaults: new { controller = "Employees", action = "Index" });

app.MapControllerRoute(
    name: "standaard-templates",
    pattern: "standaard-templates",
    defaults: new { controller = "Templates", action = "Index" });

app.MapControllerRoute(
    name: "terugblikken",
    pattern: "terugblikken",
    defaults: new { controller = "Reviews", action = "Index" });

app.MapControllerRoute(
    name: "filialen",
    pattern: "filialen",
    defaults: new { controller = "Branches", action = "Index" });

// Route for logging out
app.MapControllerRoute(
    name: "logout",
    pattern: "uitloggen",
    defaults: new { controller = "Logout", action = "Logout" });

app.Run();
