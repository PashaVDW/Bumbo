﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using bumbo.Data;

#nullable disable

namespace DataLayer.Migrations
{
    [DbContext(typeof(BumboDBContext))]
    partial class BumboDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Branch", b =>
                {
                    b.Property<int>("BranchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BranchId"));

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("HouseNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PrognosisId")
                        .HasColumnType("nvarchar(45)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("BranchId");

                    b.HasIndex("CountryName");

                    b.HasIndex("PrognosisId");

                    b.ToTable("Branches");

                    b.HasData(
                        new
                        {
                            BranchId = 1,
                            CountryName = "Netherlands",
                            HouseNumber = "10",
                            Name = "Amsterdam Branch",
                            PostalCode = "12345",
                            Street = "Damrak"
                        },
                        new
                        {
                            BranchId = 2,
                            CountryName = "Belgium",
                            HouseNumber = "20",
                            Name = "Brussels Branch",
                            PostalCode = "67890",
                            Street = "Grand Place"
                        });
                });

            modelBuilder.Entity("DataLayer.Models.Function", b =>
                {
                    b.Property<string>("FunctionName")
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.HasKey("FunctionName");

                    b.ToTable("Functions");

                    b.HasData(
                        new
                        {
                            FunctionName = "Cashier"
                        },
                        new
                        {
                            FunctionName = "Stocker"
                        },
                        new
                        {
                            FunctionName = "Manager"
                        });
                });

            modelBuilder.Entity("Employee", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("BID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("HouseNumber")
                        .HasColumnType("int");

                    b.Property<bool>("IsSystemManager")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("ManagerOfBranchId")
                        .HasColumnType("int");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("ManagerOfBranchId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1",
                            AccessFailedCount = 0,
                            BID = "B001",
                            BirthDate = new DateTime(1985, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "99f557eb-93da-4adf-9ed9-4dd6ad8d998c",
                            Email = "john.doe@example.com",
                            EmailConfirmed = true,
                            FirstName = "John",
                            HouseNumber = 10,
                            IsSystemManager = true,
                            LastName = "Doe",
                            LockoutEnabled = false,
                            ManagerOfBranchId = 1,
                            MiddleName = "A.",
                            NormalizedEmail = "JOHN.DOE@EXAMPLE.COM",
                            NormalizedUserName = "JOHN.DOE@EXAMPLE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEM2WOqvdxjL+h7v8kadgiG/fy2HHkQxn5Og3Er6YvYoIMWVOhJ2rWxJuJg3zTHQuKg==",
                            PhoneNumber = "06-9876543",
                            PhoneNumberConfirmed = false,
                            PostalCode = "12345",
                            SecurityStamp = "9f97526c-dd5f-41da-ab4c-9391ba29a68d",
                            StartDate = new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TwoFactorEnabled = false,
                            UserName = "john.doe@example.com"
                        },
                        new
                        {
                            Id = "2",
                            AccessFailedCount = 0,
                            BID = "B002",
                            BirthDate = new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "37258c27-84f2-437d-8ec5-ad9824a4a642",
                            Email = "jane.smith@example.com",
                            EmailConfirmed = true,
                            FirstName = "Jane",
                            HouseNumber = 22,
                            IsSystemManager = false,
                            LastName = "Smith",
                            LockoutEnabled = false,
                            MiddleName = "B.",
                            NormalizedEmail = "JANE.SMITH@EXAMPLE.COM",
                            NormalizedUserName = "JANE.SMITH@EXAMPLE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEIuGEM3Mky3p8PkVz9ebnMvAhVN2H/rNo1oC7RogPTTuz49csp1+Vc+iEJOx4K2E2w==",
                            PhoneNumber = "06-12345678",
                            PhoneNumberConfirmed = false,
                            PostalCode = "54321",
                            SecurityStamp = "c19cfb08-ec82-4122-98c6-1378f834c83a",
                            StartDate = new DateTime(2012, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TwoFactorEnabled = false,
                            UserName = "jane.smith@example.com"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("bumbo.Models.BranchHasEmployee", b =>
                {
                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FunctionName")
                        .HasColumnType("nvarchar(45)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("BranchId", "EmployeeId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("FunctionName");

                    b.ToTable("BranchHasEmployees");
                });

            modelBuilder.Entity("bumbo.Models.Country", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Name");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            Name = "Netherlands"
                        },
                        new
                        {
                            Name = "Belgium"
                        },
                        new
                        {
                            Name = "Germany"
                        });
                });

            modelBuilder.Entity("bumbo.Models.Days", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Name");

                    b.ToTable("Days");

                    b.HasData(
                        new
                        {
                            Name = "Maandag"
                        },
                        new
                        {
                            Name = "Dinsdag"
                        },
                        new
                        {
                            Name = "Woensdag"
                        },
                        new
                        {
                            Name = "Donderdag"
                        },
                        new
                        {
                            Name = "Vrijdag"
                        },
                        new
                        {
                            Name = "Zaterdag"
                        },
                        new
                        {
                            Name = "Zondag"
                        });
                });

            modelBuilder.Entity("bumbo.Models.Norm", b =>
                {
                    b.Property<int>("normId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("normId"));

                    b.Property<string>("activity")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("branchId")
                        .HasColumnType("int");

                    b.Property<int>("normInSeconds")
                        .HasColumnType("int");

                    b.Property<int>("week")
                        .HasColumnType("int");

                    b.Property<int>("year")
                        .HasColumnType("int");

                    b.HasKey("normId");

                    b.HasIndex("branchId", "year", "week", "activity")
                        .IsUnique();

                    b.ToTable("Norms");
                });

            modelBuilder.Entity("bumbo.Models.Prognosis", b =>
                {
                    b.Property<string>("PrognosisId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<int>("WeekNr")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("PrognosisId");

                    b.HasIndex("BranchId");

                    b.ToTable("Prognoses");

                    b.HasData(
                        new
                        {
                            PrognosisId = "1",
                            BranchId = 1,
                            WeekNr = 40,
                            Year = 2024
                        });
                });

            modelBuilder.Entity("bumbo.Models.Prognosis_has_days", b =>
                {
                    b.Property<string>("Days_name")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("PrognosisId")
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<int>("CustomerAmount")
                        .HasColumnType("int");

                    b.Property<int>("PackagesAmount")
                        .HasColumnType("int");

                    b.HasKey("Days_name", "PrognosisId");

                    b.HasIndex("PrognosisId");

                    b.ToTable("Prognosis_Has_Days");

                    b.HasData(
                        new
                        {
                            Days_name = "Maandag",
                            PrognosisId = "1",
                            CustomerAmount = 100,
                            PackagesAmount = 50
                        },
                        new
                        {
                            Days_name = "Dinsdag",
                            PrognosisId = "1",
                            CustomerAmount = 120,
                            PackagesAmount = 60
                        },
                        new
                        {
                            Days_name = "Woensdag",
                            PrognosisId = "1",
                            CustomerAmount = 130,
                            PackagesAmount = 55
                        },
                        new
                        {
                            Days_name = "Donderdag",
                            PrognosisId = "1",
                            CustomerAmount = 110,
                            PackagesAmount = 45
                        },
                        new
                        {
                            Days_name = "Vrijdag",
                            PrognosisId = "1",
                            CustomerAmount = 150,
                            PackagesAmount = 70
                        },
                        new
                        {
                            Days_name = "Zaterdag",
                            PrognosisId = "1",
                            CustomerAmount = 160,
                            PackagesAmount = 80
                        },
                        new
                        {
                            Days_name = "Zondag",
                            PrognosisId = "1",
                            CustomerAmount = 140,
                            PackagesAmount = 65
                        });
                });

            modelBuilder.Entity("bumbo.Models.Template", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Branch_branchId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("Branch_branchId");

                    b.ToTable("Template");
                });

            modelBuilder.Entity("bumbo.Models.Template_has_days", b =>
                {
                    b.Property<int>("Templates_id")
                        .HasColumnType("int");

                    b.Property<int>("ContainerAmount")
                        .HasColumnType("int");

                    b.Property<int>("CustomerAmount")
                        .HasColumnType("int");

                    b.Property<string>("Days_name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Templates_id");

                    b.HasIndex("Days_name");

                    b.ToTable("Template_has_days");
                });

            modelBuilder.Entity("Branch", b =>
                {
                    b.HasOne("bumbo.Models.Country", "Country")
                        .WithMany("Branches")
                        .HasForeignKey("CountryName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("bumbo.Models.Prognosis", null)
                        .WithMany("Branches")
                        .HasForeignKey("PrognosisId");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Employee", b =>
                {
                    b.HasOne("Branch", "ManagerOfBranch")
                        .WithMany("Employees")
                        .HasForeignKey("ManagerOfBranchId");

                    b.Navigation("ManagerOfBranch");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Employee", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Employee", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Employee", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Employee", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("bumbo.Models.BranchHasEmployee", b =>
                {
                    b.HasOne("Branch", "Branch")
                        .WithMany("BranchHasEmployees")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Employee", "Employee")
                        .WithMany("BranchEmployees")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Models.Function", "Function")
                        .WithMany()
                        .HasForeignKey("FunctionName");

                    b.Navigation("Branch");

                    b.Navigation("Employee");

                    b.Navigation("Function");
                });

            modelBuilder.Entity("bumbo.Models.Prognosis", b =>
                {
                    b.HasOne("Branch", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("bumbo.Models.Prognosis_has_days", b =>
                {
                    b.HasOne("bumbo.Models.Days", "Days")
                        .WithMany("Prognosis_Has_Days")
                        .HasForeignKey("Days_name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("bumbo.Models.Prognosis", "Prognosis")
                        .WithMany("Prognosis_Has_Days")
                        .HasForeignKey("PrognosisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Days");

                    b.Navigation("Prognosis");
                });

            modelBuilder.Entity("bumbo.Models.Template", b =>
                {
                    b.HasOne("Branch", "Branch")
                        .WithMany()
                        .HasForeignKey("Branch_branchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("bumbo.Models.Template_has_days", b =>
                {
                    b.HasOne("bumbo.Models.Days", "Days")
                        .WithMany("TemplateHasDays")
                        .HasForeignKey("Days_name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("bumbo.Models.Template", "Template")
                        .WithMany("TemplateHasDays")
                        .HasForeignKey("Templates_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Days");

                    b.Navigation("Template");
                });

            modelBuilder.Entity("Branch", b =>
                {
                    b.Navigation("BranchHasEmployees");

                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Employee", b =>
                {
                    b.Navigation("BranchEmployees");
                });

            modelBuilder.Entity("bumbo.Models.Country", b =>
                {
                    b.Navigation("Branches");
                });

            modelBuilder.Entity("bumbo.Models.Days", b =>
                {
                    b.Navigation("Prognosis_Has_Days");

                    b.Navigation("TemplateHasDays");
                });

            modelBuilder.Entity("bumbo.Models.Prognosis", b =>
                {
                    b.Navigation("Branches");

                    b.Navigation("Prognosis_Has_Days");
                });

            modelBuilder.Entity("bumbo.Models.Template", b =>
                {
                    b.Navigation("TemplateHasDays");
                });
#pragma warning restore 612, 618
        }
    }
}
