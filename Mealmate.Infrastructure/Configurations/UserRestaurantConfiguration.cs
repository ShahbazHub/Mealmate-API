using Mealmate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class UserRestaurantConfiguration : IEntityTypeConfiguration<UserRestaurant>
    {
        public void Configure(EntityTypeBuilder<UserRestaurant> builder)
        {
            builder.ToTable("UserRestaurant", "Identity");
            builder.HasKey(p => p.Id)
                   .HasName("PK_UserRestaurant");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.UserId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.RestaurantId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.IsActive)
                .HasColumnType("BIT")
                .IsRequired();

            builder.Property(p => p.Created)
                .HasColumnType("DATETIMEOFFSET")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");


            builder.HasOne(p => p.User)
                .WithMany(p => p.UserRestaurants)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_UserRestaurant_User")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Restaurant)
                .WithMany(p => p.UserRestaurants)
                .HasForeignKey(p => p.RestaurantId)
                .HasConstraintName("FK_UserRestaurant_Restaurant")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
