using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using bumbo.Data;  // Ensure the namespace matches your BumboDBContext file
using bumbo.Models;  // Ensure the namespace matches your Employee model
using DataLayer;
using DataLayer.Interfaces;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BumboDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("bumbo")));

builder.Services.AddScoped<ITemplatesRepository, TemplatesRepositorySql>();
builder.Services.AddScoped<ITemplateHasDaysRepository, TemplateHasDaysRepositorySql>();
builder.Services.AddScoped<IAvailabilityRepository, AvailabilityRepositorySql>();
builder.Services.AddScoped<IPrognosisRepository, PrognosisRepositorySql>();
builder.Services.AddScoped<ISchoolScheduleRepository, SchoolScheduleRepositorySql>();
builder.Services.AddScoped<IPrognosisHasDaysRepository, PrognosisHasDaysRepositorySql>();
builder.Services.AddScoped<INormsRepository, NormsRepositorySql>();
builder.Services.AddScoped<IFunctionRepository, FunctionRepositorySql>();
builder.Services.AddScoped<IBranchHasEmployeeRepository, BranchHasEmployeeRepositorySql>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepositorySql>();
builder.Services.AddScoped<IBranchesRepository, BranchesRepositorySql>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepositorySql>();


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
    name: "prognosis",
    pattern: "prognoses",
    defaults: new { controller = "Prognosis", action = "Index" });

app.MapControllerRoute(
    name: "scheduleManager",
    pattern: "roosterManager",
    defaults: new { controller = "ScheduleManager", action = "Index" });

app.MapControllerRoute(
    name: "forecasts",
    pattern: "prognoses",
    defaults: new { controller = "Forecasts", action = "Index" });

app.MapControllerRoute(
    name: "schoolSchedule",
    pattern: "schoolrooster/toevoegen",
    defaults: new { controller = "SchoolSchedule", action = "AddSchoolSchedule" });

app.MapControllerRoute(
    name: "norms",
    pattern: "normeringen",
    defaults: new { controller = "Norms", action = "Index" });

app.MapControllerRoute(
    name: "employees",
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
    name: "reviews",
    pattern: "terugblikken",
    defaults: new { controller = "Reviews", action = "Index" });

app.MapControllerRoute(
    name: "branches",
    pattern: "filialen",
    defaults: new { controller = "Branches", action = "BranchesView" });

app.MapControllerRoute(
    name: "logout",
    pattern: "uitloggen",
    defaults: new { controller = "Account", action = "Logout" });

app.MapControllerRoute(
    name: "login",
    pattern: "inloggen",
    defaults: new { controller = "Account", action = "Login" });

app.MapControllerRoute(
    name: "availability",
    pattern: "beschikbaarheid",
    defaults: new { controller = "Availability", action = "Index" });

app.Run();
