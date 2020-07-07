using Mealmate.DataAccess.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.DataAccess.Configurations
{
    public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.ToTable("UserLogin", "Identity");

            builder.HasKey(u => new { u.LoginProvider, u.ProviderKey });

            builder.HasOne(ur => ur.User)
                .WithMany(ur => ur.UserLogins)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        }
    }
}
