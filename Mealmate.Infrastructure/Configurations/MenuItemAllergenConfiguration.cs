using Mealmate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mealmate.Infrastructure.Configurations
{
    public class MenuItemAllergenConfiguration : IEntityTypeConfiguration<MenuItemAllergen>
    {
        public void Configure(EntityTypeBuilder<MenuItemAllergen> builder)
        {
            builder.ToTable("MenuItemAllergen", "Mealmate");
            builder.HasKey(p => p.Id)
                   .HasName("PK_MenuItemAllergen");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.MenuItemId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.Created)
                .HasColumnType("DATETIMEOFFSET")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.IsActive)
                    .HasColumnType("BIT")
                    .IsRequired();

            builder.HasOne(p => p.MenuItem)
                .WithMany(p => p.MenuItemAllergens)
                .HasForeignKey(p => p.MenuItemId)
                .HasConstraintName("FK_MenuItemAllergen_MenuItem")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
