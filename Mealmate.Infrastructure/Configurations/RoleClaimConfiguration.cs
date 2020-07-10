using Mealmate.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class RoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<int>> builder)
        {
            builder.ToTable("RoleClaim", "Identity");

            builder.HasKey(rc => rc.Id);

            //builder.HasOne(ur => ur.Role)
            //    .WithMany(ur => ur.RoleClaims)
            //    .HasForeignKey(ur => ur.RoleId)
            //    .IsRequired();
        }
    }
}
