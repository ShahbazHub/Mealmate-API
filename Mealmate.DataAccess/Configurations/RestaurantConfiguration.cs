using Mealmate.DataAccess.Entities.Mealmate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.DataAccess.Configurations
{
    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.ToTable("Restaurant", "Infrastructure");
            builder.HasKey(p => p.RestaurantId)
                   .HasName("PK_Restaurant");

            builder.Property(p => p.RestaurantId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasColumnType("NVARCHAR(250)")
                .IsRequired();

            builder.Property(p => p.Description)
                .HasColumnType("NVARCHAR(1000)")
                .IsRequired();

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
