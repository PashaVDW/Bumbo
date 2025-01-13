using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using bumbo.Data;
using bumbo.Models;
using DataLayer.Interfaces;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Razor;
using bumbo.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BumboDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("bumbo")));

builder.Services.AddScoped<ITemplatesRepository, TemplatesRepositorySql>();
builder.Services.AddScoped<ISwapShiftRequestRepository, SwapShiftRequestRepositorySql>();
builder.Services.AddScoped<ITemplateHasDaysRepository, TemplateHasDaysRepositorySql>();
builder.Services.AddScoped<IPrognosisHasDaysHasDepartments, PrognosisHasDaysHasDepartmentsSql>();
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
builder.Services.AddScoped<ICountryRepository, CountryRepositorySql>();
builder.Services.AddScoped<IDaysRepositorySQL, DaysRepositorySQL>();
builder.Services.AddScoped<IDepartmentsRepository, DepartmentsRepositorySql>();
builder.Services.AddScoped<IRegisteredHoursRepository, RegisteredHoursRepositorySQL>();

builder.Services.AddTransient<IPrognosisCalculator, PrognosisCalculator>();

builder.Services.AddIdentity<Employee, IdentityRole>()
    .AddEntityFrameworkStores<BumboDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("nl-NL"),
    };

    options.DefaultRequestCulture = new RequestCulture("nl-NL");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
});


var app = builder.Build();

app.UseMiddleware<bumbo.Middleware.LocalizationMiddleware>();

app.UseRequestLocalization();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ScheduleManager}/{action=Index}/{id?}");

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
