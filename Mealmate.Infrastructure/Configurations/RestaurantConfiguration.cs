using Mealmate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.ToTable("Restaurant", "Mealmate");
            builder.HasKey(p => p.Id)
                   .HasName("PK_Restaurant");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasColumnType("NVARCHAR(250)")
                .IsRequired();

            builder.Property(p => p.Description)
                .HasColumnType("NVARCHAR(1000)");

            builder.Property(p => p.Created)
                .HasColumnType("DATETIMEOFFSET")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(p => p.Owner)
                .WithMany(p => p.Restaurants)
                .HasForeignKey(p => p.OwnerId)
                .HasConstraintName("FK_Restaurant_User")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
