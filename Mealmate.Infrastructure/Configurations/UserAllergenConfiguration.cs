using Mealmate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class UserAllergenConfiguration : IEntityTypeConfiguration<UserAllergen>
    {
        public void Configure(EntityTypeBuilder<UserAllergen> builder)
        {
            builder.ToTable("UserAllergen", "Identity");
            builder.HasKey(p => p.Id)
                   .HasName("PK_UserAllergen");

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
                .WithMany(p => p.UserAllergens)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_UserAllergen_User")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
