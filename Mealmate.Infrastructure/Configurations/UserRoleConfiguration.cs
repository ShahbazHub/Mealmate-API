using Mealmate.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
        {
            builder.ToTable("UserRole", "Identity");

            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            //builder.HasOne(ur => ur.Role)
            //    .WithMany(ur => ur.UserRoles)
            //    .HasForeignKey(ur => ur.RoleId)
            //    .IsRequired();

            //builder.HasOne(ur => ur.User)
            //    .WithMany(ur => ur.UserRoles)
            //    .HasForeignKey(ur => ur.UserId)
            //    .IsRequired();
        }
    }
}
