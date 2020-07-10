using Mealmate.Core.Entities;
using Mealmate.Core.Entities.Base;
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
    public class MealmateContext : IdentityDbContext<User, Role, int>
    {
        public MealmateContext(DbContextOptions<MealmateContext> options)
            : base(options)
        {
        }

        private IDbContextTransaction _currentTransaction;
        public IDbContextTransaction GetCurrentTransaction => _currentTransaction;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typeToRegisters = typeof(Entity).GetTypeInfo().Assembly.DefinedTypes.Select(t => t.AsType());

            modelBuilder.RegisterEntities(typeToRegisters);

            modelBuilder.RegisterConvention();

            base.OnModelCreating(modelBuilder);

            //Lookup Schema
            modelBuilder.ApplyConfiguration(new OptionItemConfiguration());

            //Mealmate Schema
            modelBuilder.ApplyConfiguration(new BranchConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
            modelBuilder.ApplyConfiguration(new MenuConfiguration());
            modelBuilder.ApplyConfiguration(new MenuItemConfiguration());
            modelBuilder.ApplyConfiguration(new MenuItemOptionConfiguration());
            modelBuilder.ApplyConfiguration(new QRCodeConfiguration());
            modelBuilder.ApplyConfiguration(new RestaurantConfiguration());
            modelBuilder.ApplyConfiguration(new TableConfiguration());

            //Identity Schema
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RoleClaimConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserLoginConfiguration());
            modelBuilder.ApplyConfiguration(new UserClaimConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserTokenConfiguration());
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
