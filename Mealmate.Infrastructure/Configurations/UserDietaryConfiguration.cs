using Mealmate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class UserDietaryConfiguration : IEntityTypeConfiguration<UserDietary>
    {
        public void Configure(EntityTypeBuilder<UserDietary> builder)
        {
            builder.ToTable("UserDietary", "Identity");
            builder.HasKey(p => p.Id)
                   .HasName("PK_UserDietary");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.UserId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.Created)
                .HasColumnType("DATETIMEOFFSET")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");


            builder.HasOne(p => p.User)
                .WithMany(p => p.UserDietaries)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_UserDietary_User")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
