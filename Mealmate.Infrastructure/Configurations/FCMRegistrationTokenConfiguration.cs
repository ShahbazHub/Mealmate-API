using Mealmate.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class FCMRegistrationTokenConfiguration : IEntityTypeConfiguration<FCMRegistrationToken>
    {
        public void Configure(EntityTypeBuilder<FCMRegistrationToken> builder)
        {
            builder.ToTable("FCMRegistrationToken", "Identity");

            builder.HasKey(p => p.Id)
               .HasName("PK_FCMRegistrationToken");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.CreationDate)
               .HasColumnType("DATETIMEOFFSET")
               .IsRequired()
               .HasDefaultValueSql("GETDATE()");
        }
    }
}
