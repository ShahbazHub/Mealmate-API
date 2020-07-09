using Mealmate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.DataAccess.Configurations
{
    public class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            builder.ToTable("UserClaim", "Identity");
            
            builder.HasKey(uc => uc.Id);

            builder.HasOne(ur => ur.User)
                .WithMany(ur => ur.UserClaims)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        }
    }
}
