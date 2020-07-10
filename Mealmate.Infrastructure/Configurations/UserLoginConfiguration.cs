using Mealmate.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class UserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserLogin<int>> builder)
        {
            builder.ToTable("UserLogin", "Identity");

            builder.HasKey(u => new { u.LoginProvider, u.ProviderKey });

            //builder.HasOne(ur => ur.User)
            //    .WithMany(ur => ur.UserLogins)
            //    .HasForeignKey(ur => ur.UserId)
            //    .IsRequired();
        }
    }
}
