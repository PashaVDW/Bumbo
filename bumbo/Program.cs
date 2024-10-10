using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using bumbo.Data;  // Ensure the namespace matches your BumboDBContext file
using bumbo.Models;  // Ensure the namespace matches your Employee model

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BumboDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("anthony")));

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

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Custom routes for specific pages (in Dutch)
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

// Seed roles and admin user on startup
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Employee>>();

    // Seed "Admin" role if not exists
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // Seed an admin user if not exists
    var adminEmail = "admin@company.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        adminUser = new Employee
        {
            UserName = adminEmail,
            Email = adminEmail,
            FirstName = "Admin",
            LastName = "User",
            EmailConfirmed = true,
            BID = "B001",
            BirthDate = new DateTime(1980, 1, 1),
            PostalCode = "12345",
            HouseNumber = 1,
            StartDate = DateTime.Now,
            FunctionName = "Admin",
            IsSystemManager = true
        };

        await userManager.CreateAsync(adminUser, "AdminPassword123!");
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}

app.Run();
