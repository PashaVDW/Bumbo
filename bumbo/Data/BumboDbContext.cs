using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using bumbo.Models;

namespace bumbo.Data
{
    public class BumboDBContext : IdentityDbContext<Employee>  // Extend IdentityDbContext<Employee>
    {
        public BumboDBContext(DbContextOptions<BumboDBContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }  // This will now map to the Identity users

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Adjusted seed data for Identity fields (remove non-Identity fields like password here)
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = "1", // This is the IdentityUser's primary key
                    BID = "B001",
                    FirstName = "John",
                    MiddleName = "A.",
                    LastName = "Doe",
                    BirthDate = new DateTime(1985, 2, 20),
                    PostalCode = "12345",
                    HouseNumber = 10,
                    Email = "john.doe@example.com",  // Identity's Email property
                    UserName = "john.doe@example.com",  // Identity's Username
                    EmailConfirmed = true,  // Identity requires this to confirm email
                    StartDate = new DateTime(2010, 1, 1),
                    FunctionName = "Manager",
                    IsSystemManager = true
                },
                new Employee
                {
                    Id = "2", // IdentityUser primary key
                    BID = "B002",
                    FirstName = "Jane",
                    MiddleName = "B.",
                    LastName = "Smith",
                    BirthDate = new DateTime(1990, 5, 15),
                    PostalCode = "54321",
                    HouseNumber = 22,
                    Email = "jane.smith@example.com",  // Identity's Email property
                    UserName = "jane.smith@example.com",  // Identity's Username
                    EmailConfirmed = true,
                    StartDate = new DateTime(2012, 4, 1),
                    FunctionName = "Cashier",
                    IsSystemManager = false
                }
            );
        }
    }
}
