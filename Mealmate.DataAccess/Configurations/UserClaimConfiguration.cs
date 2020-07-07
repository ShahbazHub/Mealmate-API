using Mealmate.DataAccess.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
