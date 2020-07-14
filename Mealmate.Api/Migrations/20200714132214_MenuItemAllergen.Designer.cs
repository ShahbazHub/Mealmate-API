﻿// <auto-generated />
using System;
using Mealmate.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Mealmate.Api.Migrations
{
    [DbContext(typeof(MealmateContext))]
    [Migration("20200714132214_MenuItemAllergen")]
    partial class MenuItemAllergen
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Mealmate.Core.Entities.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("NVARCHAR(1000)");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIMEOFFSET")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("INT");

                    b.HasKey("Id")
                        .HasName("PK_Branch");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Branch","Mealmate");
                });

            modelBuilder.Entity("Mealmate.Core.Entities.CuisineType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIMEOFFSET")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)");

                    b.HasKey("Id")
                        .HasName("PK_CuisineType");

                    b.ToTable("CuisineType","Lookup");
                });

            modelBuilder.Entity("Mealmate.Core.Entities.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BranchId")
                        .HasColumnType("INT");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIMEOFFSET")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)");

                    b.HasKey("Id")
                        .HasName("PK_Location");

                    b.HasIndex("BranchId");

                    b.ToTable("Location","Mealmate");
                });

            modelBuilder.Entity("Mealmate.Core.Entities.Lookup.Allergen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIMEOFFSET")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)");

                    b.Property<byte[]>("Photo")
                        .IsRequired()
                        .HasColumnType("VARBINARY(MAX)");

                    b.HasKey("Id")
                        .HasName("PK_Allergen");

                    b.ToTable("Allergen","Lookup");
                });

            modelBuilder.Entity("Mealmate.Core.Entities.Lookup.Dietary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIMEOFFSET")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)");

                    b.Property<byte[]>("Photo")
                        .IsRequired()
                        .HasColumnType("VARBINARY(MAX)");

                    b.HasKey("Id")
                        .HasName("PK_Dietary");

                    b.ToTable("Dietary","Lookup");
                });

            modelBuilder.Entity("Mealmate.Core.Entities.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BranchId")
                        .HasColumnType("INT");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIMEOFFSET")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)");

                    b.Property<TimeSpan>("ServiceTime")
                        .HasColumnType("TIME(7)");

                    b.HasKey("Id")
                        .HasName("PK_Menu");

                    b.HasIndex("BranchId");

                    b.ToTable("Menu","Mealmate");
                });

            modelBuilder.Entity("Mealmate.Core.Entities.MenuItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIMEOFFSET")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MenuId")
                        .HasColumnType("INT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("varbinary(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("DECIMAL(10, 2)");

                    b.HasKey("Id")
                        .HasName("PK_MenuItem");

                    b.HasIndex("MenuId");

                    b.ToTable("MenuItem","Mealmate");
                });

            modelBuilder.Entity("Mealmate.Core.Entities.MenuItemAllergen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AllergenId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIMEOFFSET")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("MenuItemId")
                        .HasColumnType("INT");

                    b.HasKey("Id")
                        .HasName("PK_MenuItemAllergen");

                    b.HasIndex("AllergenId");

                    b.HasIndex("MenuItemId");

                    b.ToTable("MenuItemAllergen","Mealmate");
                });

            modelBuilder.Entity("Mealmate.Core.Entities.MenuItemOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIMEOFFSET")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("MenuItemId")
                        .HasColumnType("INT");

                    b.Property<int>("OptionItemId")
                        .HasColumnType("INT");

                    b.Property<decimal>("Price")
                        .HasColumnType("DECIMAL(10, 2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("INT");

                    b.HasKey("Id")
                        .HasName("PK_MenuItemOption");

                    b.HasIndex("MenuItemId");

                    b.HasIndex("OptionItemId");

                    b.ToTable("MenuItemOption","Mealmate");
                });

            modelBuilder.Entity("Mealmate.Core.Entities.OptionItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BranchId")
                        .HasColumnType("INT");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIMEOFFSET")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)");

                    b.HasKey("Id")
                        .HasName("PK_OptionItem");

                    b.HasIndex("BranchId");

                    b.ToTable("OptionItem","Lookup");
                });

            modelBuilder.Entity("Mealmate.Core.Entities.QRCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Code")
                        .IsRequired()
                        .HasColumnType("VARBINARY(MAX)");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIMEOFFSET")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("TableId")
                        .HasColumnType("INT");

                    b.HasKey("Id")
                        .HasName("PK_QRCode");

                    b.HasIndex("TableId");

                    b.ToTable("QRCode","Mealmate");
                });

            modelBuilder.Entity("Mealmate.Core.Entities.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIMEOFFSET")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("CuisineTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("NVARCHAR(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("VARBINARY(MAX)");

                    b.HasKey("Id")
                        .HasName("PK_Restaurant");

                    b.HasIndex("CuisineTypeId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Restaurant","Mealmate");
                });

            modelBuilder.Entity("Mealmate.Core.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIMEOFFSET")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Role","Identity");
                });

            modelBuilder.Entity("Mealmate.Core.Entities.Table", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIMEOFFSET")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("LocationId")
                        .HasColumnType("INT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)");

                    b.HasKey("Id")
                        .HasName("PK_Table");

                    b.HasIndex("LocationId");

                    b.ToTable("Table","Mealmate");
                });

            modelBuilder.Entity("Mealmate.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIMEOFFSET")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(350)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)");

                    b.Property<string>("LastName")
                        .HasColumnType("NVARCHAR(250)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("VARCHAR(25)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(25)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("User","Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int?>("RoleId1")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("RoleId1");

                    b.ToTable("RoleClaim","Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId1")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("UserClaim","Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId1")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("UserLogin","Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int?>("RoleId1")
                        .HasColumnType("int");

                    b.Property<int?>("UserId1")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("RoleId1");

                    b.HasIndex("UserId1");

                    b.ToTable("UserRole","Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("UserId1")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LoginProvider", "UserId", "Name");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("UserToken","Identity");
                });

            modelBuilder.Entity("Mealmate.Core.Entities.Branch", b =>
                {
                    b.HasOne("Mealmate.Core.Entities.Restaurant", "Restaurant")
                        .WithMany("Branches")
                        .HasForeignKey("RestaurantId")
                        .HasConstraintName("FK_Branch_Restaurant")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Mealmate.Core.Entities.Location", b =>
                {
                    b.HasOne("Mealmate.Core.Entities.Branch", "Branch")
                        .WithMany("Locations")
                        .HasForeignKey("BranchId")
                        .HasConstraintName("FK_Location_Branch")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Mealmate.Core.Entities.Menu", b =>
                {
                    b.HasOne("Mealmate.Core.Entities.Branch", "Branch")
                        .WithMany("Menus")
                        .HasForeignKey("BranchId")
                        .HasConstraintName("FK_Menu_Branch")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Mealmate.Core.Entities.MenuItem", b =>
                {
                    b.HasOne("Mealmate.Core.Entities.Menu", "Menu")
                        .WithMany("MenuItems")
                        .HasForeignKey("MenuId")
                        .HasConstraintName("FK_MenuItem_Menu")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Mealmate.Core.Entities.MenuItemAllergen", b =>
                {
                    b.HasOne("Mealmate.Core.Entities.Lookup.Allergen", "Allergen")
                        .WithMany()
                        .HasForeignKey("AllergenId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Mealmate.Core.Entities.MenuItem", "MenuItem")
                        .WithMany("MenuItemAllergens")
                        .HasForeignKey("MenuItemId")
                        .HasConstraintName("FK_MenuItemAllergen_MenuItem")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Mealmate.Core.Entities.MenuItemOption", b =>
                {
                    b.HasOne("Mealmate.Core.Entities.MenuItem", "MenuItem")
                        .WithMany("MenuItemOptions")
                        .HasForeignKey("MenuItemId")
                        .HasConstraintName("FK_MenuItemOption_MenuItem")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Mealmate.Core.Entities.OptionItem", "OptionItem")
                        .WithMany("MenuItemOptions")
                        .HasForeignKey("OptionItemId")
                        .HasConstraintName("FK_MenuItemOption_OptionItem")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Mealmate.Core.Entities.OptionItem", b =>
                {
                    b.HasOne("Mealmate.Core.Entities.Branch", "Branch")
                        .WithMany("OptionItems")
                        .HasForeignKey("BranchId")
                        .HasConstraintName("FK_OptionItem_Branch")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Mealmate.Core.Entities.QRCode", b =>
                {
                    b.HasOne("Mealmate.Core.Entities.Table", "Table")
                        .WithMany("QRCodes")
                        .HasForeignKey("TableId")
                        .HasConstraintName("FK_QRCode_Table")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Mealmate.Core.Entities.Restaurant", b =>
                {
                    b.HasOne("Mealmate.Core.Entities.CuisineType", "CuisineType")
                        .WithMany("Restaurants")
                        .HasForeignKey("CuisineTypeId")
                        .HasConstraintName("FK_Restaurant_CuisineType")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Mealmate.Core.Entities.User", "Owner")
                        .WithMany("Restaurants")
                        .HasForeignKey("OwnerId")
                        .HasConstraintName("FK_Restaurant_User")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Mealmate.Core.Entities.Table", b =>
                {
                    b.HasOne("Mealmate.Core.Entities.Location", "Location")
                        .WithMany("Tables")
                        .HasForeignKey("LocationId")
                        .HasConstraintName("FK_Table_Location")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Mealmate.Core.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Mealmate.Core.Entities.Role", null)
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId1");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Mealmate.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Mealmate.Core.Entities.User", null)
                        .WithMany("UserClaims")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Mealmate.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Mealmate.Core.Entities.User", null)
                        .WithMany("UserLogins")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Mealmate.Core.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Mealmate.Core.Entities.Role", null)
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId1");

                    b.HasOne("Mealmate.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Mealmate.Core.Entities.User", null)
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Mealmate.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Mealmate.Core.Entities.User", null)
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId1");
                });
#pragma warning restore 612, 618
        }
    }
}
