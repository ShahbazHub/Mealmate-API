using Mealmate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.ToTable("RoleClaim", "Identity");
            
            builder.HasKey(rc => rc.Id);

            builder.HasOne(ur => ur.Role)
                .WithMany(ur => ur.RoleClaims)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        }
    }
}
