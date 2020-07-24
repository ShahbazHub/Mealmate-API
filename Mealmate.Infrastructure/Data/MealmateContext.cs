using Mealmate.Core.Entities;
using Mealmate.Core.Entities.Base;
using Mealmate.Core.Entities.Lookup;
using Mealmate.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Mealmate.Infrastructure.Data
{
    public class MealmateContext : IdentityDbContext<User, Role, int,
        UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public MealmateContext(DbContextOptions<MealmateContext> options)
            : base(options)
        {
        }

        private IDbContextTransaction _currentTransaction;
        public IDbContextTransaction GetCurrentTransaction => _currentTransaction;

        // User items
        public DbSet<UserRestaurant> UserRestaurants { get; set; }
        public DbSet<UserAllergen> UserAllergens { get; set; }
        public DbSet<UserDietary> UserDietaries { get; set; }

        // Restaurant
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Location> Locaations { get; set; }
        public DbSet<Table> Tables { get; set; }

        // Menus
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<MenuItemAllergen> MenuItemAllergens { get; set; }
        public DbSet<MenuItemDietary> MenuItemDietaries { get; set; }
        public DbSet<MenuItemOption> MenuItemOptions { get; set; }

        // Lookups
        public DbSet<Allergen> Allergens { get; set; }
        public DbSet<Dietary> Dietaries { get; set; }
        public DbSet<CuisineType> CuisineTypes { get; set; }
        public DbSet<OptionItem> OptionItems { get; set; }
        public DbSet<OptionItemAllergen> OptionItemAllergens { get; set; }
        public DbSet<OptionItemDietary> OptionItemDietaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typeToRegisters = typeof(Entity).GetTypeInfo().Assembly.DefinedTypes.Select(t => t.AsType());

            modelBuilder.RegisterEntities(typeToRegisters);

            modelBuilder.RegisterConvention();
            base.OnModelCreating(modelBuilder);

            //Lookup Schema
            modelBuilder.ApplyConfiguration(new OptionItemConfiguration());
            modelBuilder.ApplyConfiguration(new OptionItemAllergenConfiguration());
            modelBuilder.ApplyConfiguration(new OptionItemDietaryConfiguration());

            //Mealmate Schema
            modelBuilder.ApplyConfiguration(new RestaurantConfiguration());
            modelBuilder.ApplyConfiguration(new BranchConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
            modelBuilder.ApplyConfiguration(new TableConfiguration());

            modelBuilder.ApplyConfiguration(new MenuConfiguration());
            modelBuilder.ApplyConfiguration(new MenuItemConfiguration());
            modelBuilder.ApplyConfiguration(new MenuItemOptionConfiguration());
            modelBuilder.ApplyConfiguration(new QRCodeConfiguration());

            //Identity Schema
            modelBuilder.ApplyConfiguration(new UserAllergenConfiguration());
            modelBuilder.ApplyConfiguration(new UserDietaryConfiguration());

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RoleClaimConfiguration());

            modelBuilder.ApplyConfiguration(new UserLoginConfiguration());
            modelBuilder.ApplyConfiguration(new UserClaimConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserTokenConfiguration());

            modelBuilder.ApplyConfiguration(new UserRestaurantConfiguration());

            // Lookups
            modelBuilder.ApplyConfiguration(new DietaryConfiguration());
            modelBuilder.ApplyConfiguration(new AllergenConfiguration());
            modelBuilder.ApplyConfiguration(new CuisineTypeConfiguration());

            modelBuilder.ApplyConfiguration(new MenuItemAllergenConfiguration());
            modelBuilder.ApplyConfiguration(new MenuItemDietaryConfiguration());


            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemDetailConfiguration());

            // Custom
            modelBuilder.RegisterCustomMappings(typeToRegisters);
        }

        public async Task BeginTransactionAsync()
        {
            _currentTransaction = _currentTransaction ?? await Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();
                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }

    static class AspnetRunContextConfigurations
    {
        internal static void RegisterEntities(this ModelBuilder modelBuilder, IEnumerable<Type> typeToRegisters)
        {
            var entityTypes = typeToRegisters.Where(t => (t.GetTypeInfo().IsSubclassOf(typeof(Entity)) || t.GetTypeInfo().IsSubclassOf(typeof(Enumeration))) && !t.GetTypeInfo().IsAbstract);

            foreach (var type in entityTypes)
            {
                modelBuilder.Entity(type);
            }
        }

        internal static void RegisterConvention(this ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (entity.ClrType.Namespace != null)
                {
                    var tableName = entity.ClrType.Name;
                    var typeBuilder = modelBuilder.Entity(entity.Name);
                    typeBuilder.ToTable(tableName);

                    if (entity.ClrType.IsSubclassOf(typeof(Entity)))
                    {
                    }
                    else if (entity.ClrType.IsSubclassOf(typeof(Enumeration)))
                    {
                        typeBuilder.Property("Id").ValueGeneratedNever();
                    }
                }
            }

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        internal static void RegisterCustomMappings(this ModelBuilder modelBuilder, IEnumerable<Type> typeToRegisters)
        {
            var customModelBuilderTypes = typeToRegisters.Where(x => typeof(ICustomModelBuilder).IsAssignableFrom(x));
            foreach (var builderType in customModelBuilderTypes)
            {
                if (builderType != null && builderType != typeof(ICustomModelBuilder))
                {
                    var builder = (ICustomModelBuilder)Activator.CreateInstance(builderType);
                    builder.Build(modelBuilder);
                }
            }
        }
    }
}
