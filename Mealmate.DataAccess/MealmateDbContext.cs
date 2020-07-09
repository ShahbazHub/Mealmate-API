using Mealmate.DataAccess.Configurations;
using Mealmate.DataAccess.Entities.Identity;
using Mealmate.DataAccess.Entities.Mealmate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Mealmate.DataAccess
{
    public class MealmateDbContext : IdentityDbContext<User, Role, int,
        UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        private readonly IConfigurationRoot _config;

        public MealmateDbContext(DbContextOptions<MealmateDbContext> options,
            IConfigurationRoot config) : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // ConnectionStrings__MealmateConnectionString
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:MealmateConnectionString"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ASP.NET Core Identity
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserLoginConfiguration());
            modelBuilder.ApplyConfiguration(new UserTokenConfiguration());
            modelBuilder.ApplyConfiguration(new UserClaimConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RoleClaimConfiguration());

            // Mealmate tables
            modelBuilder.ApplyConfiguration(new RestaurantConfiguration());
            modelBuilder.ApplyConfiguration(new BranchConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
            modelBuilder.ApplyConfiguration(new TableConfiguration());
            modelBuilder.ApplyConfiguration(new QRCodeConfiguration());
            modelBuilder.ApplyConfiguration(new MenuConfiguration());
            modelBuilder.ApplyConfiguration(new MenuItemConfiguration());
            modelBuilder.ApplyConfiguration(new MenuItemOptionConfiguration());

            // Lookups
            modelBuilder.ApplyConfiguration(new OptionItemConfiguration());

        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<QRCode> QRCodes { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<MenuItemOption> MenuItemOptions { get; set; }

    }
}
