using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using bumbo.Data;  // Ensure the namespace matches your BumboDBContext file
using bumbo.Models;  // Ensure the namespace matches your Employee model
using DataLayer;
using DataLayer.Interfaces;
using DataLayer.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BumboDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("anthony")));

builder.Services.AddScoped<INormsRepository, NormsRepositorySql>();
builder.Services.AddScoped<IFunctionRepository, FunctionRepositorySql>();
builder.Services.AddScoped<IBranchHasEmployeeRepository, BranchHasEmployeeRepositorySql>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepositorySql>();

builder.Services.AddIdentity<Employee, IdentityRole>()
    .AddEntityFrameworkStores<BumboDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

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
    name: "createEmployee",
    pattern: "medewerkers/aanmaken",
    defaults: new { controller = "Employees", action = "Create" });

app.MapControllerRoute(
    name: "updateEmployee",
    pattern: "medewerkers/bewerken",
    defaults: new { controller = "Employees", action = "Update" });

app.MapControllerRoute(
    name: "templates",
    pattern: "standaard-templates",
    defaults: new { controller = "Templates", action = "Index" });

app.MapControllerRoute(
    name: "terugblikken",
    pattern: "terugblikken",
    defaults: new { controller = "Reviews", action = "Index" });

app.MapControllerRoute(
    name: "filialen",
    pattern: "filialen",
    defaults: new { controller = "Branches", action = "BranchesView" });

// Route for logging out
app.MapControllerRoute(
    name: "logout",
    pattern: "uitloggen",
    defaults: new { controller = "Logout", action = "Logout" });

app.Run();
