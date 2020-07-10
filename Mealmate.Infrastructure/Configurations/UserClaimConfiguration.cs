using Mealmate.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class UserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserClaim<int>> builder)
        {
            builder.ToTable("UserClaim", "Identity");
            builder.HasKey(uc => uc.Id);

            //builder.HasOne(ur => ur.User)
            //    .WithMany(ur => ur.UserClaims)
            //    .HasForeignKey(ur => ur.UserId)
            //    .IsRequired();
        }
    }
}
