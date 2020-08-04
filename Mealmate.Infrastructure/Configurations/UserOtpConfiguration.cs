using Mealmate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class UserOtpConfiguration : IEntityTypeConfiguration<UserOtp>
    {
        public void Configure(EntityTypeBuilder<UserOtp> builder)
        {
            builder.ToTable("UserOtp", "Identity");
            builder.HasKey(p => p.Id)
                   .HasName("PK_UserOtp");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.UserId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.StartTime)
                .HasColumnType("TIME(7)")
                .IsRequired();

            builder.Property(p => p.EndTime)
                .HasColumnType("TIME(7)")
                .IsRequired();
            
            builder.Property(p => p.IsActive)
                    .HasColumnType("BIT")
                    .IsRequired();

            builder.HasOne(p => p.User)
                .WithMany(p => p.UserOtps)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_UserOtp_User")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
