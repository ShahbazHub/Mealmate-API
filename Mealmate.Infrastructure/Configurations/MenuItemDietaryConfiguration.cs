using Mealmate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class MenuItemDietaryConfiguration : IEntityTypeConfiguration<MenuItemDietary>
    {
        public void Configure(EntityTypeBuilder<MenuItemDietary> builder)
        {
            builder.ToTable("MenuItemDietary", "Mealmate");
            builder.HasKey(p => p.Id)
                   .HasName("PK_MenuItemDietary");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.MenuItemId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.Created)
                .HasColumnType("DATETIMEOFFSET")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");


            builder.HasOne(p => p.MenuItem)
                .WithMany(p => p.MenuItemDietaries)
                .HasForeignKey(p => p.MenuItemId)
                .HasConstraintName("FK_MenuItemDietary_MenuItem")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
