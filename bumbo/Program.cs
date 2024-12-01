using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using bumbo.Data; // Ensure the namespace matches your BumboDBContext file
using bumbo.Models; // Ensure the namespace matches your Employee model
using DataLayer.Interfaces;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BumboDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("bumbo")));

// Register repositories
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
builder.Services.AddScoped<IBranchRequestsEmployeeRepository, BranchRequestsEmployeeRepositorySql>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepositorySql>();
builder.Services.AddScoped<ILabourRulesRepository, LabourRulesRepositorySql>();
builder.Services.AddScoped<IDepartmentsRepository, DepartmentsRepositorySql>();

// Configure Identity
builder.Services.AddIdentity<Employee, IdentityRole>()
    .AddEntityFrameworkStores<BumboDBContext>()
    .AddDefaultTokenProviders();

// Add Localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] {
        new CultureInfo("en-US"),
        new CultureInfo("nl-NL"),
    };

    options.DefaultRequestCulture = new RequestCulture("nl-NL");
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

app.UseRequestLocalization();

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

// Define default and custom routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "prognosis",
    pattern: "prognoses",
    defaults: new { controller = "Prognosis", action = "Index" });

app.MapControllerRoute(
    name: "scheduleManager",
    pattern: "roosterManager",
    defaults: new { controller = "ScheduleManager", action = "Index" });

app.MapControllerRoute(
    name: "schoolSchedule",
    pattern: "schoolrooster",
    defaults: new { controller = "SchoolSchedule", action = "Index" });

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
    name: "requests",
    pattern: "verzoeken",
    defaults: new { controller = "Requests", action = "Index" });

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
