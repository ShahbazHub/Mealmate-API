using Mealmate.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshToken", "Identity");

            builder.HasKey(p => p.Id)
               .HasName("PK_RefreshToken");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
